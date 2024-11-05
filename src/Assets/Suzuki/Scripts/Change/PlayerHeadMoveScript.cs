using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerHeadMoveScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;

    Change change;
    CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        change = GameObject.FindObjectOfType<Change>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }
    public IEnumerator MoveHead(Vector3 start, Vector3 end, GameObject changeObj)
    {
        transform.LookAt(end);
        ChangeCameraTarget(transform);

        float totalTime = Vector3.Distance(start, end) / moveSpeed;
        float timer = 0f;

        while(timer < totalTime)
        {
            timer += Time.deltaTime;

            Vector3 position = Vector3.Lerp(start, end, timer / totalTime);
            transform.position = position;
            yield return null;
        }
        change.ChangeCameraTarget(changeObj);
        //ChangeCameraTarget(change.transform);
        Destroy(gameObject);
    }

    void ChangeCameraTarget(Transform target)
    {
        virtualCamera.Follow = target;
        virtualCamera.LookAt = target;
    }
}
