using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerHeadMoveScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    CinemachineVirtualCamera virtualCamera;

    CinemachineFramingTransposer framingTransposer;
    float headDistance = 5.0f;
    float playerDistance = 0.3f;

    PlayerDamageEffect damageEffect;
    //GameObject changeTarget;
    void Start()
    {
        Debug.Log("Start");
    }

    /// <summary>
    /// 乗り移る対象に向かって頭を動かし、乗り移る
    /// </summary>
    /// <param name="start">開始地点</param>
    /// <param name="target">乗り移る対象</param>
    /// <param name="headOffset">頭の高さ</param>
    /// <returns></returns>
    public IEnumerator MoveHead(Vector3 start, Transform target, Vector3 headOffset, GameObject changeObj, Change change)
    {
        damageEffect = GameObject.FindObjectOfType<PlayerDamageEffect>();
        damageEffect.Reset();

        transform.LookAt(target);
        ChangeCameraTarget(transform, headDistance);

        float totalTime = Vector3.Distance(start, target.position) / moveSpeed;
        float timer = 0f;

        while(timer < totalTime && !PauseManager.IsPaused)
        {
            timer += Time.unscaledDeltaTime;

            Vector3 position = Vector3.Lerp(start, target.position, timer / totalTime);
            transform.position = position + headOffset;
            yield return null;
        }

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        CharacterStatus targetStatus = changeObj.GetComponent<CharacterStatus>();
        targetStatus.OnPossess();
        TargetManeger.PlayerStatus.CharacterAnimator.updateMode = AnimatorUpdateMode.Normal;

        yield return new WaitForSecondsRealtime(0.5f);

        //if (TargetManeger.getPlayerObj().TryGetComponent<Animator>(out Animator playerAnimator))
        //{
        //    playerAnimator.SetBool("Change", false);
        //}
        //changeTarget = changeObj;
        TargetManeger.PlayerStatus.CharacterAnimator.SetBool("Change", false);
        change.ChangeCameraTarget(changeObj);
        ChangeCameraTarget(change.gameObject.transform, playerDistance);
        Destroy(gameObject);
    }

    //public void ReturnCameraTarget(Change change)
    //{
    //    if (changeTarget != null)
    //    {
    //        change.ChangeCameraTarget(changeTarget);
    //        ChangeCameraTarget(change.gameObject.transform, playerDistance);
    //        Destroy(gameObject);
    //    }
    //}

    void ChangeCameraTarget(Transform target,float distance)
    {
        Debug.Log("target:" + target.name);

        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        virtualCamera.Follow = target;
        virtualCamera.LookAt = target;
        framingTransposer.m_CameraDistance = distance;
    }
}
