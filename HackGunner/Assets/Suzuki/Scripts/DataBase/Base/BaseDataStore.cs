using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDataStore<T, U> : MonoBehaviour where T : BaseDataBase<U> where U : BaseData
{
    [SerializeField] protected T dataBase;

    // プロパティ
    public T DataBase
    {
        get { return dataBase; }
    }

    /// <summary>
    /// 文字列からデータベース内のデータを取得
    /// </summary>
    public U FindWithName(string name)
    {
        if(string.IsNullOrEmpty(name)) { return default; }// Nullチェック
        return dataBase.ItemList.Find(e => e.Name == name);
    }

    /// <summary>
    /// IDからデータベース内のデータを取得
    /// </summary>
    public U FindWithId(int id)
    {
        return dataBase.ItemList.Find(e => e.Id == id);
    }
}
