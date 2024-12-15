using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadManager : MonoBehaviour
{
    [Header("頭（敵の場合は親の頭）"), SerializeField] GameObject head;
    [Header("敵の頭、敵のみ設定"), SerializeField] MeshRenderer enemyHead;
    [Header("プレイヤーの頭、敵のみ設定"), SerializeField] MeshRenderer playerHead;
    
    public void OnHeadThrow()// animatorから呼び出される
    {
        head.SetActive(false);
        TargetManeger.StartHeadChange();
    }

    public void OnHeadLand()
    {
        head.SetActive(true);

        // 敵の場合のみの設定
        if (enemyHead != null && playerHead != null)
        {
            enemyHead.enabled = false;
            playerHead.enabled = true;
        }
    }
}
