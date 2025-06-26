using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveryArea : MonoBehaviour
{
    [SerializeField]
    EnemyDiscoveryController[] enemy;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            foreach (EnemyDiscoveryController enemies in enemy)
            {
                if(enemies != null)
                    enemies.IsDiscobery();
            }
        }
    }
}
