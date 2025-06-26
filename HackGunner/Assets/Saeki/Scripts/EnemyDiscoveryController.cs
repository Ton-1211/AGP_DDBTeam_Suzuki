using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiscoveryController : EnemyBaseClass
{
    private bool discovery;
    public void IsDiscobery() {  discovery = true; }
    protected override void SetUpOverride()
    {
        discovery = false;
    }

    protected override Vector3 GetTargetPos()
    {
        if(discovery)
        {
            return TargetSetting.transform.position;
        }
        
        if (FindCheck())
        {
            discovery = true;
        }
        return this.transform.position;
    }
}
