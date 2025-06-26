using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadManager : MonoBehaviour
{
    [Header("“ªi“G‚Ìê‡‚Íe‚Ì“ªj"), SerializeField] GameObject head;
    [Header("“G‚Ì“ªA“G‚Ì‚İİ’è"), SerializeField] MeshRenderer enemyHead;
    [Header("ƒvƒŒƒCƒ„[‚Ì“ªA“G‚Ì‚İİ’è"), SerializeField] MeshRenderer playerHead;
    
    public MeshRenderer EnemyHead => enemyHead;
    public void OnHeadThrow()// animator‚©‚çŒÄ‚Ño‚³‚ê‚é
    {
        head.SetActive(false);
        TargetManeger.StartHeadChange();
    }

    public void OnHeadLand()
    {
        head.SetActive(true);

        // “G‚Ìê‡‚Ì‚İ‚Ìİ’è
        if (enemyHead != null && playerHead != null)
        {
            enemyHead.enabled = false;
            playerHead.enabled = true;
        }
    }
}
