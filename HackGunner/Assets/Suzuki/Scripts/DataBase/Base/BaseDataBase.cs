using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDataBase<T> : ScriptableObject where T : BaseData
{
    [SerializeField] List<T> itemList = new List<T>();

    // �v���p�e�B
    public List<T> ItemList
    {
        get { return itemList; }
    }

    void OnValidate()// �C���X�y�N�^�[��ŕύX���������Ƃ�
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            itemList[i].SetId(i);// ���X�g�ɓo�^����Ă��鏇��ID��U�蒼��
        }
    }
}
