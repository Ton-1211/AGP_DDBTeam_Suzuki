using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] NavMeshObstacle navMeshObstacle;
    [Header("破壊できるタグ"),SerializeField] string breakableTag = "Breakable";
    [Header("床を破壊するまでの重なり具合"), Range(0, 1), SerializeField] float breakFloorThreshold = 0.8f;
    [SerializeField] LayerMask breakableLayer;
    [SerializeField] LayerMask enemyLayer;

    List<GameObject> breakFloors = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        BreakFloor();
    }

    void BreakFloor()
    {
        float sphereRadius = transform.localScale.x * navMeshObstacle.radius;
        float breakDistanceThreshold = sphereRadius * breakFloorThreshold;
        Collider[] hitStage = Physics.OverlapSphere(transform.position, sphereRadius, breakableLayer);

        foreach(Collider hit in hitStage)
        {
            if(hit.tag != breakableTag) continue;

            float distanceSquare = new Vector2(hit.transform.position.x - transform.position.x, hit.transform.position.z - transform.position.z).sqrMagnitude;
            if(distanceSquare <= breakDistanceThreshold * breakDistanceThreshold)
            {
                breakFloors.Add(hit.gameObject);
            }
        }

        for(int i = breakFloors.Count - 1; i >= 0; i--)
        {
            Destroy(breakFloors[i]);
            breakFloors.Remove(breakFloors[i]);
        }

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, sphereRadius, enemyLayer);
        foreach(Collider enemy in hitEnemies)
        {
            if(enemy.TryGetComponent<CharacterStatus>(out CharacterStatus character))
            {
                character.TakeDamage(10000f, true);// 致死ダメージを与える
            }
        }
    }
}
