using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript_Scrapped : MonoBehaviour
{
    [SerializeField] Transform[] targetPoints;
    [SerializeField] bool patrol = false;
    NavMeshAgent agent;
    int destPoint = 0;
    float minDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        if (targetPoints.Length > 0)
        {
            GoToNextPoint();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!agent.pathPending && agent.remainingDistance <= minDistance)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        if(targetPoints.Length < 0 || (patrol && targetPoints.Length <= destPoint))
        {
            return;
        }

        agent.destination = targetPoints[destPoint].position;

        // 次のポイント地点設定
        destPoint = patrol ? (destPoint + 1) % targetPoints.Length : destPoint + 1;
        if(destPoint >= targetPoints.Length)
        {
            destPoint = targetPoints.Length - 1;
        }
    }
}
