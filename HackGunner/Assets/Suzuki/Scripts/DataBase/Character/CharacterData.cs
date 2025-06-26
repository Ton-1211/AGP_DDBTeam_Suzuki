using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(menuName = "OriginalScriptableObjects/Data/CharacterData")]
public class CharacterData : BaseData
{
    [SerializeField] float maxHp;
    [SerializeField] float maxPossessTime;
    [SerializeField] float immunityTime = 0f;

    // プロパティ
    public float MaxHp
    {
        get { return maxHp; }
    }
    public float MaxPossessTime
    {
        get { return maxPossessTime;}
    }
    public float ImmunityTime
    {
        get { return immunityTime;}
    }
}
