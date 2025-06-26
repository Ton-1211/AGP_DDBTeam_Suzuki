using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDataBase<T> : ScriptableObject where T : BaseData
{
    [SerializeField] List<T> itemList = new List<T>();

    // プロパティ
    public List<T> ItemList
    {
        get { return itemList; }
    }

    void OnValidate()// インスペクター上で変更があったとき
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            itemList[i].SetId(i);// リストに登録されている順にIDを振り直す
        }
    }
}
