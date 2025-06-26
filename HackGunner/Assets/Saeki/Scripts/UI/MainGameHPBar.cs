using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameHPBar: MonoBehaviour
{
    [SerializeField] Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void AnimatorHPBarRemove()
    {
        animator.SetBool("BarDirected", false);
    }

    public void AnimatorHPBarEnter()
    {
        animator.SetBool("BarDirected", true);
    }
}
