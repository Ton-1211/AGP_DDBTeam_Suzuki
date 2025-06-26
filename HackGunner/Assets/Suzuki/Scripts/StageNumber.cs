using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OriginalScriptableObjects/StageNumber")]
public class StageNumber : ScriptableObject
{
    [SerializeField]int num;

    public int Num
    {
        get { return num; }
        set { num = value; }
    }
}
