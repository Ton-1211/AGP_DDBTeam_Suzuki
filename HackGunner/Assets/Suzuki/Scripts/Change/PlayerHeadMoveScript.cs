using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerHeadMoveScript : MonoBehaviour
{
    const float HeadDistance = 5.0f;
    const float PlayerCameraDistance = 0.3f;
    const uint SpinCount = 5;// 頭が着くまでに回転する回数
    const int FullRotationDegrees = 360;
    const float WaitAfterHeadChanged = 0.5f;

    [SerializeField] float moveSpeed = 10f;

    CinemachineVirtualCamera virtualCamera;

    CinemachineFramingTransposer framingTransposer;
    float headDistance = HeadDistance;
    float playerCameraDistance = PlayerCameraDistance;

    PlayerDamageEffect damageEffect;
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer= gameObject.GetComponent<MeshRenderer>();
    }
    /// <summary>
    /// 乗り移る対象に向かって頭を動かし、乗り移る
    /// </summary>
    /// <param name="start">開始地点</param>
    /// <param name="target">乗り移る対象</param>
    /// <param name="headOffset">頭の高さ</param>
    /// <param name="changeObj">乗り移る対象のオブジェクト</param>
    /// <param name ="change">乗り移りを行うスクリプト</param>
    /// <returns>処理が完了するまでのコルーチン</returns>
    public IEnumerator MoveHead(Vector3 start, Transform target, Vector3 headOffset, GameObject changeObj, Change change)
    {
        damageEffect = GameObject.FindObjectOfType<PlayerDamageEffect>();
        damageEffect.Reset();// HPが減っているときのエフェクトを消す

        transform.LookAt(target);
        SetCameraTarget(transform, headDistance);

        float totalTime = Vector3.Distance(start, target.position) / moveSpeed;// 開始地点からターゲットまでの移動にかかる時間
        float rotate = FullRotationDegrees * SpinCount / totalTime;// 1秒あたりの回転量(度)
        float timer = 0f;

        // 頭を目標地点まで飛ばす
        while (timer < totalTime)
        {
            // ポーズ中
            if (!PauseManager.IsPaused)
            {
                timer += Time.unscaledDeltaTime;

                // 移動と回転を行う
                Vector3 position = Vector3.Lerp(start, target.position, timer / totalTime);
                transform.position = position + headOffset;
                transform.Rotate(new Vector3(0f, 1f, 0f) * rotate * Time.unscaledDeltaTime, Space.World);
            }
            yield return null;
        }

        // 頭が到達したあと
        meshRenderer.enabled = false;// 飛ばす演出用の頭を見えなくする

        CharacterStatus targetStatus = changeObj.GetComponent<CharacterStatus>();// ステータスを取得
        targetStatus.OnPossess();// プレイヤーのステータスに切り替える

        TargetManeger.PlayerStatus.CharacterAnimator.updateMode = AnimatorUpdateMode.Normal;// アニメーションの再生モードを切り替え

        yield return new WaitForSecondsRealtime(WaitAfterHeadChanged);// アニメーション開始までに待機（ゲームスピードに関係ない秒数で待機するようにしている）

        TargetManeger.PlayerStatus.CharacterAnimator.SetBool("Change", false);// 乗り移り後のアニメーションを再生

        // カメラを乗り移り後の敵にフォーカス
        change.ChangeCameraTarget(changeObj);
        SetCameraTarget(change.gameObject.transform, playerCameraDistance);

        Destroy(gameObject);// 飛ばす演出用の頭を消去
    }

    void SetCameraTarget(Transform target,float distance)
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();// VirtualCamera（ポインタ）を取得

        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();// FramingTransposer（ポインタ）を取得

        // 取得したポインタの中身を変更
        virtualCamera.Follow = target;// カメラの追尾先を変更
        virtualCamera.LookAt = target;// 〃の注目先を変更
        framingTransposer.m_CameraDistance = distance;// 〃のカメラ距離を変更
    }
}
