using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindWithPlayerShootScript : MonoBehaviour
{
    [SerializeField] EnemyChaseController enemyChaseController;
    [SerializeField] PlayerRay playerRay;
    [SerializeField] PlayerMove player;
    [SerializeField] float findableRange;
    [SerializeField] float minimumLostDistance = 4f;

    Change change;

    // Start is called before the first frame update
    void Start()
    {
        change = FindObjectOfType<Change>();
    }

    void FixedUpdate()
    {
        if (playerRay.Shoot)
        {
            DetectPlayerShoot();
        }
        if(change.Changed)
        {
            DisableChase();
        }
    }

    void DetectPlayerShoot()
    {
        float distanceSquare = (player.transform.position - transform.position).sqrMagnitude;
        if(distanceSquare <= findableRange * findableRange && !enemyChaseController.enabled)
        {
            enemyChaseController.enabled = true;
        }
    }

    void DisableChase()
    {
        float distanceSquare = (player.transform.position - transform.position).sqrMagnitude;
        if (distanceSquare >= minimumLostDistance * minimumLostDistance)
        {
            enemyChaseController.enabled = false;
        }
    }
}
