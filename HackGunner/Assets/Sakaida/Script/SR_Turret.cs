using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_Turret : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject TurretBody;

    float Dir;

    public bool FindPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        isAttck();
        isSearch();
        isDirMathf();
    }

    private void isDirMathf()
    {
        Dir = Vector2.Distance((Vector2)Player.transform.position, (Vector2)transform.position);
    }

    void isAttck() 
    {
        if (FindPlayer) 
        {
            float TurretDir = Vector2.Distance(Player.transform.position, transform.position);
            //TurretBody.transform.up = TurretDir;
        }
    }
    void isSearch() 
    {
        if (Dir < 5)
        {
            FindPlayer = true;
            Debug.Log("GA");
        }
        else 
        { 
        FindPlayer= false;
        }
        
    }
}
