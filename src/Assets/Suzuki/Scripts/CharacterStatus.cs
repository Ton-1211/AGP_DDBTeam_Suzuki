using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] CharacterData characterData;

    float hp;
    float remainPossessTime;
    float damageTimer;
    Animator animator;
    bool possessed;
    public float Hp
    {
        get { return hp; }
    } 
    public float RemainPossessTime
    {
        get { return remainPossessTime; }
    }
    public bool IsDead
    {
        get { return hp <= 0; }
    }
    public bool CanPossess// ���ڂ�邩�ǂ���
    {
        get { return IsDead || possessed; }
    }
    public string ObjectTag
    {
        get { return gameObject.tag; }
    }
    public Animator CharacterAnimator
    {
        get { return animator; }
    }
    void Start()
    {
        possessed = false;
        StartSetUp();
    }

    void Update()
    {
        SearchAnimator();
        if(damageTimer > 0f)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    public void StartSetUp()
    {
        SetHpMax();
        TryGetComponent<Animator>(out animator);
        remainPossessTime = characterData.MaxPossessTime;
        damageTimer = 0f;
        if(tag == "Player")
        {
            possessed = true;
        }
    }

    /// <summary>
    /// �_���[�W��^����
    /// </summary>
    public void TakeDamage(float damage, bool launch = false)
    {
        if (damageTimer > 0f) return;// ���G���Ԓ��̓_���[�W�������Ȃ�
        hp -= damage;
        if(hp <= 0f)
        {
            hp = 0f;
            if(launch && TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddForce(Vector3.up * 15f, ForceMode.Impulse);
            }
        }

        if(gameObject.tag == "Player")
        {
            damageTimer = characterData.ImmunityTime;
        }

        animator.SetBool("Dead", IsDead);
    }

    /// <summary>
    /// ���߂����Ԃ��o��
    /// </summary>
    public void ElapsePossessTime()
    {
        remainPossessTime -= Time.fixedDeltaTime;
        if(remainPossessTime <= 0f)
        {
            remainPossessTime = 0f;
        }
    }

    public void OnPossess()// ���߂����̏���
    {
        if(!possessed)// ������߂��̂Ƃ�
        {
            SetHpMax();
            possessed = true;
        }

        animator.SetBool("Dead", IsDead);
    }
    public void OnPossessToOther()
    {
        animator.SetBool("Dead", true);
    }
    void SetHpMax()
    {
        hp = characterData.MaxHp;// HP���ő�ɐݒ�
    }

    void SearchAnimator()
    {
        if (animator != null && animator.gameObject != null &&
            animator.gameObject == gameObject) return;// ������Ɛݒ肳��Ă���i�ύX���Ȃ��j�Ȃ�Ď擾���Ȃ�

        TryGetComponent<Animator>(out animator);
    }
}
