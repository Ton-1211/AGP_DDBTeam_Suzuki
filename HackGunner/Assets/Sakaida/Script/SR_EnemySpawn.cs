using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_EnemySpawn : MonoBehaviour
{

    [SerializeField] GameObject ChaseEnemy;
    [SerializeField] GameObject patrolEnemy;
    [SerializeField] GameObject StayEnemy;

    public bool Spawn= false;

    public List<GameObject> SpawnPoint = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !Spawn) 
        {
            isSpawn();
        }
    }
    public void isSpawn() 
    { 
        
        Spawn = true;
    foreach (GameObject obj in SpawnPoint) 
        { 
        GameObject CL_Enemy = Instantiate(ChaseEnemy, obj.transform.position,Quaternion.identity);
        }
    }
}
