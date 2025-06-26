using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseController : EnemyBaseClass
{
    protected override Vector3 GetTargetPos() { return TargetSetting.transform.position; }
}