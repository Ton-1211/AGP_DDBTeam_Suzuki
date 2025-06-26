using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable] public class EnemyProperties
{
    [SerializeField] EnemyBaseClass enemyClass;
    [SerializeField] SearchColliderScript searchColliderScript;

    public EnemyBaseClass EnemyClass
    {
        get { return enemyClass; }
    }

    public SearchColliderScript SearchColliderScript
    {
        get { return searchColliderScript; }
        set { searchColliderScript = value; }
    }

    //public void SetSearchColliderScript(SearchColliderScript searchColliderScript)
    //{
    //    this.searchColliderScript = searchColliderScript;
    //}
}

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float resetDistance;
    [SerializeField] List<EnemyProperties> enemyList;
    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// �v���C���[�𔭌����Ă���G�̔����������Z�b�g����
    /// </summary>
    public void ResetSearch(Vector3 playerPosition)
    {
        // Change�X�N���v�g������ڂ莞�ɌĂяo���\��
        foreach(EnemyProperties enemy in enemyList)
        {
            if(enemy.SearchColliderScript.FoundPlayer != null)
            {
                float distanceSquare = (playerPosition - transform.position).sqrMagnitude;
                if (distanceSquare >= resetDistance * resetDistance)
                {
                    enemy.SearchColliderScript.OnPlayerChange();
                }
            }
        }
    }

#if UNITY_EDITOR
    void OnValidate()// �C���X�y�N�^�[��̕ύX��
    {
        if(enemyList != null)
        {
            foreach(var enemy in enemyList)
            {
                if(enemy != null)
                {
                    enemy.SearchColliderScript = enemy.EnemyClass.GetComponentInChildren<SearchColliderScript>();
                }
            }
        }
    }
#endif
}
