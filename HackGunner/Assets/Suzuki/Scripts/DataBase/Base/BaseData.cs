using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseData : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] int id;

    // プロパティ
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Id
    {
        get { return id; }
    }

    public void SetId(int value)
    {
        id = value;
    }
}
