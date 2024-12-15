using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadManager : MonoBehaviour
{
    [Header("���i�G�̏ꍇ�͐e�̓��j"), SerializeField] GameObject head;
    [Header("�G�̓��A�G�̂ݐݒ�"), SerializeField] MeshRenderer enemyHead;
    [Header("�v���C���[�̓��A�G�̂ݐݒ�"), SerializeField] MeshRenderer playerHead;
    
    public void OnHeadThrow()// animator����Ăяo�����
    {
        head.SetActive(false);
        TargetManeger.StartHeadChange();
    }

    public void OnHeadLand()
    {
        head.SetActive(true);

        // �G�̏ꍇ�݂̂̐ݒ�
        if (enemyHead != null && playerHead != null)
        {
            enemyHead.enabled = false;
            playerHead.enabled = true;
        }
    }
}
