using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_TankMove : MonoBehaviour
{
    // Start is called before the first frame update

    public bool Move = false;
    float Speed = 0;
    float SpeedPP = 0.005f;

    [SerializeField] GameObject Head;
    [SerializeField] GameObject Syhou;

    public Vector3 TankRotate;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Head.transform.rotation = Quaternion.Euler(0, TankRotate.x, 0);
        Syhou.transform.rotation = Quaternion.Euler(TankRotate.y, TankRotate.x, 0);
        if (TankRotate.y > 0)
        {
            TankRotate.y = 0;
        }
        else if (TankRotate.y < -17) 
        {
            TankRotate.y = -17;
        }


    }



    void isAutoMoveDEMO() 
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Move = true;
        }
        if (Move)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Speed * Time.deltaTime);
            if (Speed < 0.5)
            {
                Speed -= SpeedPP;
            }

        }
    }
}
