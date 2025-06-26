using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OriginalScriptableObjects/Data/BulletData")]
public class BulletData : BaseData
{
    [SerializeField] float attackPower;
    [SerializeField] float speed = 50f;
    [SerializeField] GameObject prefab;
    // プロパティ
    public float AttackPower
    {
        get { return attackPower; }
    }
    public float Speed
    {
        get { return speed; }
    }
    public GameObject Prefab
    {
        get { return prefab; }
    }
}
