using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Player_Controller : MonoBehaviour
{

    public float NomalSpeed = 4;
    public float AttackSpeed = 3;
    public float NoAttackSpeed = 1.5f;
    public float Speed = 3;
    public bool Attack = false;

    [SerializeField] AudioClip Clip;
    [SerializeField] AudioClip Clip2;

    SR_SoundController soundController@=>SR_SoundController.instance;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        isMove();
        isAttack();
    }

    void isMove() 
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
        }
    }
    void isAttack() 
    {
        if (Input.GetMouseButton(0))
        {
            Attack = true;
            
        }
        else 
        { 
        Attack= false;
            Speed = NomalSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall")&& Attack) 
        {
            Debug.Log("BBB");
        SR_Wall sR_Wall = other.GetComponent<SR_Wall>();
            if (sR_Wall.wallType == SR_Wall.WallType.NomalWall) 
            {
                Speed = AttackSpeed;
                soundController.PlaySEOnce(Clip);
                sR_Wall.wallType = SR_Wall.WallType.None;
            }
            if (sR_Wall.wallType == SR_Wall.WallType.SturdyWall)
            {
                Speed = NoAttackSpeed;
                soundController.PlaySEOnce(Clip2);
            }
        }
    }
}
