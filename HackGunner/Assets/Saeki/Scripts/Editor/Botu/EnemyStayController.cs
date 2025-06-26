using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStayController : MonoBehaviour
{
    public int HP = 1;
    public int remainingBullets;
    [SerializeField] private NavMeshAgent Agent;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float distance = 12f;
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float fireIntarval = 3f;
    [SerializeField] private SearchColliderScript collScript;
    [SerializeField] private Rigidbody rb;
    private float timeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        Agent.speed = moveSpeed;
    }
    public void LostHitPoint()
    {
        Agent.enabled = false;
        rb.isKinematic = false;

    }

    void TargetChase()
    {
        if (Agent.enabled)
        {
            if (Agent.pathStatus == NavMeshPathStatus.PathInvalid)
                Destroy(this.gameObject);
            else
                Agent.destination = this.transform.position;
        }
    }
    void StopChase()
    {
        //Agent.remainingDistance < distance
        if (collScript.IsFindPlayer)
        {
            Agent.speed = 0f;
            // ƒ^[ƒQƒbƒg‚Ì•ûŒü‚Ö‚Ì‰ñ“]
            Vector3 direction = Target.transform.position - transform.position;
            direction.y = 0.0f;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed);

            timeCount += Time.deltaTime;
        }
        else
        {
            timeCount = 0f;
            Agent.speed = moveSpeed;
        }
    }

    void OnFire()
    {
        remainingBullets--;
        timeCount = 0f;
        Debug.Log("FIRE!!");
        GameObject.Instantiate(Bullet, transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.tag == "Enemy" && HP > 0)
        {
            TargetChase();
            //float roteBefore = transform.rotation.y;
            StopChase();
            //float roteValue = transform.rotation.y - roteBefore;

            if (timeCount > fireIntarval && remainingBullets > 0)
                OnFire();
        }
        else
        LostHitPoint();
    }
}
