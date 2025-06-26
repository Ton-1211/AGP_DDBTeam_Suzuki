using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DarwNavMesh : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnDrawGizmosSelected()
    {
        if (agent.hasPath)
        {
            Vector3[] corners = agent.path.corners;
            Vector3 fromPoint = transform.position;
            foreach (var toPoint in agent.path.corners)
            {
                Gizmos.DrawLine(fromPoint, toPoint);
                Gizmos.DrawWireSphere(toPoint, 0.4f);
                fromPoint = toPoint;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath)
        {
            Vector3[] corners = agent.path.corners;
            //Debug.Log(corners.Length);
            return;
            Vector3 fromPoint = transform.position;
            foreach(var toPoint in agent.path.corners)
            {
                Gizmos.DrawLine(fromPoint, toPoint);
                Gizmos.DrawWireSphere(toPoint, 0.2f);
                fromPoint = toPoint;
            }
            /*
              // Œo˜H‚ð‰ÂŽ‹‰»‚·‚é‚½‚ß‚Éƒ‰ƒCƒ“‚ð•`‰æ‚·‚é
            for (int i = 0; i < corners.Length - 1; i++)
            {
                Debug.DrawLine(corners[i], corners[i + 1], Color.red);
            }
             */

        }
    }
}
