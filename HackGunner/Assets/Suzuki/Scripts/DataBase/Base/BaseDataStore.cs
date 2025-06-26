using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDataStore<T, U> : MonoBehaviour where T : BaseDataBase<U> where U : BaseData
{
    [SerializeField] protected T dataBase;

    // �v���p�e�B
    public T DataBase
    {
        get { return dataBase; }
    }

    /// <summary>
    /// �����񂩂�f�[�^�x�[�X���̃f�[�^���擾
    /// </summary>
    public U FindWithName(string name)
    {
        if(string.IsNullOrEmpty(name)) { return default; }// Null�`�F�b�N
        return dataBase.ItemList.Find(e => e.Name == name);
    }

    /// <summary>
    /// ID����f�[�^�x�[�X���̃f�[�^���擾
    /// </summary>
    public U FindWithId(int id)
    {
        return dataBase.ItemList.Find(e => e.Id == id);
    }
}
