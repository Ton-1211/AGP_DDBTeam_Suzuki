using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBaseClass : MonoBehaviour
{
    const float OffsetY = 0.5f;
    const Vector3 RotateEuler = (90, 0, 0);
    const float PlayerDamage = 1f;
    const float HitRadius = 0.3f;

    // ---------------------------------------------------------------------------------------
    // こちらの変数郡は、私（鈴木十音）の担当範囲外です。
    // ただし、命名規則が統一されていなかったため、
    // 変数名をキャメルケースに修正しました。
    // インスペクタ上での変更を維持するため、
    // 修正したものにはUnityEngine.Serialization.FormerlySerializedAsの属性を付加しています。
    // ---------------------------------------------------------------------------------------
    [UnityEngine.Serialization.FormerlySerializedAs("DestroyIntarval")][SerializeField] private float destroyInterval = 10f;
    [UnityEngine.Serialization.FormerlySerializedAs("BulletPower")][SerializeField] private float bulletPower = 60;
    [UnityEngine.Serialization.FormerlySerializedAs("PlayerDamageClip")][SerializeField] AudioClip playerDamageClip;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private BulletData bulletData;
    [SerializeField] private Material playerBulletMaterial;
    [SerializeField] private Material enemyBulletMaterial;
    [UnityEngine.Serialization.FormerlySerializedAs("EffectObject")][SerializeField] private GameObject effectObject;
    [Header("ヒット時のエフェクト"), SerializeField] private ParticleSystem particle;
    [Header("弾が衝突するレイヤー"), SerializeField] private LayerMask hitLayerMask;
    [Header("弾が消滅するレイヤー"), SerializeField] private LayerMask lapseLayerMask;

    SR_SoundController sound => SR_SoundController.instance;

    private float destroyTime = 0;
    Vector3 forward;

    // Start is called before the first frame update
    void Start()
    {
        if (this.tag == "PlayerBullet")
        {
            //Forward = transform.forward;
            GetComponent<MeshRenderer>().material = playerBulletMaterial;
        }
        else
        {
            //Forward = TargetManeger.getPlayerObj().transform.position - transform.position + Vector3.up * OffsetY;
            GetComponent<MeshRenderer>().material = enemyBulletMaterial;
            effectObject.SetActive(true);
        }

        // ==================================================
        // 担当：鈴木十音
        // 概要：弾丸の回転設定と移動量設定
        // ==================================================

        // 正面方向を取得し、正規化
        forward = transform.forward;
        forward.Normalize();

        // 弾丸を進行方向に回転させる
        Quaternion look = Quaternion.LookRotation(forward);// 弾丸が進行方向に前を向けるように
        transform.rotation = look * Quaternion.Euler(RotateEuler);// プレハブ上で弾丸が回転してしまっているため、それを直しつつ適用

        forward *= bulletPower;// 一秒あたりの移動量を計算
        // ===== ここまで =====
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime += Time.deltaTime;

        // 一定時間以上飛んでいる弾丸は削除する
        if (destroyTime > destroyInterval)
        {
            destroyTime = 0f;
            Destroy(this.gameObject);
        }

        // ==================================================
        // 担当：鈴木十音
        // 概要：弾丸の衝突確認と移動
        // ==================================================

        CheckHit(deltaTime);// 当たり判定の確認(transform.positionで動かしていて、OnTriggerEnterが反応しない場合があるので)

        float deltaTime = tag == "PlayerBullet" ? Time.unscaledDeltaTime : Time.deltaTime;// プレイヤーの弾はスロー中でも飛び方を変えない
        transform.position += forward * deltaTime;// オブジェクトの移動
        // ===== ここまで =====
    }

    // ==================================================
    // 担当：鈴木十音
    // 概要：弾丸の衝突処理、比較用メソッドと判定処理
    // ==================================================

    private void OnTriggerEnter(Collider other)
    {
        int otherLayer = other.gameObject.layer;// 衝突した相手のレイヤーを取得
        if (CompareLayer(hitLayerMask, otherLayer))// 衝突したとき
        {
            // ステージにぶつかった場合、削除
            if (CompareLayer(lapseLayerMask, otherLayer)) Destroy(this.gameObject);

            // キャラクターに当たったとき
            if (other.TryGetComponent<CharacterStatus>(out CharacterStatus character))
            {
                if (HitTagCheck(other.tag))// 弾のtagと衝突した相手のtagが違うとき（プレイヤーの弾が敵に、敵の弾がプレイヤーに当たったとき）
                {
                    // プレイヤーに対してのダメージ
                    if (character.ObjectTag == "Player")
                    {
                        sound.PlaySEOnce(playerDamageClip);// プレイヤーの被弾音を再生
                        character.TakeDamage(PlayerDamage);// ダメージを与える
                    }

                    // それ以外（敵やドラム缶）に対するダメージ
                    else
                    {
                        character.TakeDamage(bulletData.AttackPower, false);// ダメージを与える
                    }
                    Instantiate(particle, transform.position, Quaternion.identity);// 弾丸がヒットしたときのパーティクルを生成
                    Destroy(this.gameObject);// 弾丸を削除
                }
            }
        }
    }

    // 衝突したLayerがLayerMaskに含まれているか確認
    private bool CompareLayer(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    // 弾丸のタグによってヒットする相手かを判定
    private bool HitTagCheck(string otherTag)
    {
        // プレイヤーが発射した弾丸なら、的中したものがプレイヤー以外かどうか
        if (this.tag == "PlayerBullet")
            return otherTag != "Player";
        // 敵が発射した弾丸なら、的中したものが敵以外かどうか
        else
            return otherTag != "Enemy";
    }

    // 当たり判定の処理
    private void CheckHit(float deltaTime)
    {
        // SphereCastで弾丸(現在位置から時間あたりの移動先まで)に当たる範囲のオブジェクト一覧を取得
        Ray moveCheckRay = new Ray(transform.position, forward);
        RaycastHit[] hits = Physics.SphereCastAll(moveCheckRay.origin, HitRadius, moveCheckRay.direction, moveCheckRay.direction.magnitude * deltaTime, hitLayerMask);
        List<RaycastHit> hitCharacterList = new List<RaycastHit>();

        for (int i = hits.count; i > 0; i--)
        {
            // 範囲内のオブジェクトがキャラクターや破壊可能な障害物だったとき
            if (CompareLayer(LayerMask.GetMask("Enemy", "Player", "Destructive"), hits[i].transform.gameObject.layer))
            {
                hitCharacterList.Add(raycastHit);// 当たった対象一覧に追加
                hit.RemoveAt(i);// 二重で当たらないように元のリストから要素を削除
            }
        }

        foreach (RaycastHit hitCharacter in hitCharacterList)
        {
            OnTriggerEnter(hitCharacter.collider);// 衝突したときの処理（キャラクターや破壊可能な障害物、ステージの衝突処理で消える前に行いたいため）
        }

        foreach (RaycastHit hit in hits)
        {
            OnTriggerEnter(hit.collider);// 衝突したときの処理（ステージ）
        }
    }
    // ===== ここまで =====
}
