using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolController : EnemyBaseClass
{ 
    [SerializeField] private float nextPosDistance = 4f;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private bool isRoop = true;
   
    private int nextPoint = 0;
    protected override Vector3 GetTargetPos() { return NextPointUpdate(); }

    private void GoNextPoint()
    {
        // 配列内の次の位置を目標地点に設定
        nextPoint++;

        // 一巡したら最初の地点に移動
        if (isRoop && nextPoint == wayPoints.Length)
        {
            nextPoint = 0;
        }
    }
    private Vector3 NextPointUpdate()
    {
        
        // 地点がなにも設定されていないときに返す
        if (wayPoints.Length == 0)
            return this.transform.position;

        else if (wayPoints.Length == 1)
        {
            return wayPoints[0].position;
        }

        else if (GetDistanseForNavmesh() < nextPosDistance)
            GoNextPoint();
        
        // エージェントが現在設定された目標地点に行くように設定
        return wayPoints[nextPoint].position;
    }
}
