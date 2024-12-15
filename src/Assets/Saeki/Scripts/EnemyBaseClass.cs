using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseClass : CharacterStatus
{
    [SerializeField] protected GameObject Target;
    [SerializeField] private SearchColliderScript collScript;
    [SerializeField] private Rigidbody rb;
    [SerializeField] protected NavMeshAgent Agent;
    [SerializeField] private Material material;

    [SerializeField] protected float moveSpeed = 3.5f;
    [SerializeField] protected float rotationSpeed = 0.1f;

    [SerializeField] private GunStatus guns;
    [SerializeField] private GameObject gunObject;
    [SerializeField] private int remainingBullets;

    [SerializeField] protected float lockonIntarval = 3f;

    [Header("敵が使用するコントローラー"), SerializeField] RuntimeAnimatorController enemyAliveController;
    [Header("プレイヤーが乗り移ったときに使用するコントローラー"), SerializeField] RuntimeAnimatorController playerController;

    protected float remainingCount = 0, lockonCount = 0;
    protected bool remainingCheck = false, lockonCheck = false;

    private float WatchCount = 0;
    private bool isWatched = false ,isGetMesh = false, isFire = false;
    private float fireTimer = 0f, fireTimerMax = 0.2f;
    private MeshRenderer mesh;

    public void Watch() { isWatched = true; WatchCount = 0; }


    public GameObject TargetSetting
    {
        get { return Target; }  //取得用
        private set { Target = value; } //値入力用
    }
    /// <summary>
    /// 外部からのTargetの変更
    /// </summary>
    public void ChangeTarget(GameObject Set) { TargetSetting = Set; }

    protected float GetDistanseForNavmesh()
    {
        if (Agent.pathPending)
            return float.MaxValue;
        return
            Agent.remainingDistance;
    }

    void Start()
    {
        Target = GameObject.FindWithTag("Player");
        Agent.speed = moveSpeed;
        if(this.TryGetComponent<MeshRenderer>(out MeshRenderer Mesh))
        {
            mesh = Mesh; isGetMesh = true;
        }
        Agent.destination = GetTargetPos();
        StartSetUp();//基底クラスの処理
    }

    protected virtual void SetUpOverride()
    {
        return;
    }

    public void LostHitPoint()
    {
        Agent.enabled = false;
        rb.isKinematic = false;

        //仮死亡処理(3Dモデル実装後削除)
        if (isGetMesh && mesh.material != material)
        {
            mesh.material = material;
        }
    }

    void TargetChase()
    {
        if (Agent.enabled && Agent.isOnNavMesh)
        {
            CorrectTargetPlayer();

            if (Agent.pathStatus == NavMeshPathStatus.PathInvalid)
            { 
                this.gameObject.SetActive(false); 
            }
            else
            {
                if (isWatched)
                {
                    WatchCount += Time.deltaTime;
                    if(WatchCount > lockonIntarval)
                    {
                        isWatched = false;
                    }
                }

                Agent.destination = GetPoison();
            }        
        }
    }


    private Vector3 GetPoison()
    {
        if (isWatched)
            return Target.transform.position;

        return GetTargetPos(); 
    }
    protected virtual Vector3 GetTargetPos() 
    {
        return this.transform.position; 
    }
    
    void CorrectTargetPlayer()
    {
        if(!Target.CompareTag("Player"))
        {
            Target = GameObject.FindWithTag("Player");
        }
    }

    void OnFire()
    {
        remainingCount = 0f;
        if(guns.Shoot(gunObject.transform.position, gunObject.transform.forward, this.tag, true))
        {
            remainingBullets--;
            if (!remainingCheck) remainingCheck = true;
            TargetManeger.WatchTarget();

            isFire = true;
            fireTimer = fireTimerMax;
            //Debug.Log("FIRE!!");
        }
        //GameObject.Instantiate(Bullet, transform.position, Quaternion.identity);
    }

    public bool FindCheck() { return (collScript.IsFindPlayer || lockonCheck); }

    protected virtual void StopChase()
    {
        //Agent.remainingDistance < distance
        if (FindCheck())
        {
            Agent.speed = 0f;
            // ターゲットの方向への回転
            Vector3 direction = Target.transform.position - transform.position;
            direction.y = 0.0f;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            remainingCount += Time.deltaTime;
            
            if(lockonCount < lockonIntarval) 
            {
                lockonCount += Time.deltaTime;
                lockonCheck = true;
            }
            else
            {
                lockonCheck = false;
            }
        }
        else
        {
            remainingCheck = false;
            remainingCount = 0f;
            lockonCount = 0f;
            Agent.speed = moveSpeed;
        }
    }

    public bool HealthCheck() 
    {
        //Debug.Log(Hp);
        return this.gameObject.tag == "Enemy" && !CanPossess; 
    }

    private bool ShotCheck()
    { 
        if(remainingCheck)
            return remainingCount > guns.DefaultIntarval && remainingBullets > 0;
        else
            return remainingCount > guns.FirstIntarval && remainingBullets > 0;
    }

    private void MoveEnemy()
    {
        if (HealthCheck())
        {
            TargetChase();
            StopChase();
            if (ShotCheck())
                OnFire();

            FireAnimationCheck();
        }
        else
        {
            LostHitPoint();
            if(CharacterAnimator.runtimeAnimatorController == enemyAliveController)// アニメーターコントローラーの差し替え
            {
                GetComponent<Animator>().runtimeAnimatorController = playerController;
                CharacterAnimator.SetBool("Dead", true);
            }
        }
    }

    private void FireAnimationCheck()
    {
        if(isFire)
        {
            fireTimer -= Time.deltaTime;
            if(fireTimer <= 0)
            {
                isFire = false;
            }
        }
        CharacterAnimator.SetBool("Fire", isFire);
        CharacterAnimator.SetBool("AmmoKeep", guns.RemainBullets > 0);
        CharacterAnimator.SetInteger("WeaponCategory", (int)guns.WeaponType);
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }
}
