using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBaseClass : MonoBehaviour
{
    //[SerializeField] private GameObject Player;
    [SerializeField] private float DestroyIntarval = 10f;
    [SerializeField] private float BulletPower = 60;
    private float DestroyTime = 0;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField] private BulletData bulletData;
    [Header("ヒット時のエフェクト"), SerializeField] private ParticleSystem particle;
    [Header("弾が衝突するレイヤー"), SerializeField] private LayerMask hitLayerMask;
    [Header("弾が消滅するレイヤー"), SerializeField] private LayerMask lapseLayerMask;

    Vector3 Forward;
    // Start is called before the first frame update
    void Start()
    {

        if (this.tag == "PlayerBullet")
        {
            Forward = transform.forward;
        }
        else
        {
            Forward = TargetManeger.getPlayerObj().transform.position - transform.position + Vector3.up * 0.5f;
        }

        Forward.Normalize();

        Quaternion look = Quaternion.LookRotation(Forward);
        transform.rotation = look * Quaternion.Euler(90, 0, 0);

        Forward *= BulletPower;
        //rb.AddForce(Forward, ForceMode.Impulse);
        //Debug.Log(Forward * BulletPower +""+""+ rb.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyTime += Time.deltaTime;

        if (DestroyTime > DestroyIntarval)
        {
            DestroyTime = 0f;
            Destroy(this.gameObject);
        }

        float deltaTime = tag == "PlayerBullet" ? Time.unscaledDeltaTime : Time.deltaTime;// プレイヤーの弾はスロー中でも飛び方を変えない
                                                                                          //transform.Translate(Forward * BulletPower * deltaTime);
        CheckHit(deltaTime);// 当たり判定の確認(transform.positionで動かしているため)
        // オブジェクトの移動
        transform.position += Forward * deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Player")
        //{
        //    Debug.Log("Dameged!");
        //    Destroy(this.gameObject);
        //}

        //Debug.Log(CompareLayer(layerMask, other.gameObject.layer) + "layer:" + other.gameObject.layer);
        int otherLayer = other.gameObject.layer;
        if (CompareLayer(hitLayerMask, otherLayer))// 衝突したとき
        {
            if (CompareLayer(lapseLayerMask, otherLayer))
                Destroy(this.gameObject);

            if (other.TryGetComponent<CharacterStatus>(out CharacterStatus character))// キャラクターに当たったとき
            {
                if (HitTagCheck(other.tag))// 弾のtagと衝突した相手のtagが違うとき（プレイヤーの弾が敵に、敵の弾がプレイヤーに当たったとき）
                {
                    if (character.ObjectTag == "Player")
                    {
                        character.TakeDamage(1f);// 将来ダメージ種ごとでダメージの値を変えるかも
                    }
                    else
                    {
                        character.TakeDamage(bulletData.AttackPower, true);
                    }
                    Instantiate(particle, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    // 衝突したLayerがLayerMaskに含まれているか確認
    private bool CompareLayer(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    private bool HitTagCheck(string otherTag)
    {
        if (this.tag == "PlayerBullet")
            return otherTag == "Enemy";
        else
            return otherTag == "Player";
    }

    private void CheckHit(float deltaTime)
    {
        Ray moveCheckRay = new Ray(transform.position, Forward);
        if (Physics.Raycast(moveCheckRay.origin, moveCheckRay.direction, out RaycastHit hit, moveCheckRay.direction.magnitude * deltaTime, hitLayerMask))
        {
            OnTriggerEnter(hit.collider);
        }
    }
}
