using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_RotateFloor : MonoBehaviour
{
    [SerializeField] float Speed = 10f;
    [SerializeField] float MoveArea = 3;
    Vector3 SavePos;

    public enum Movemode 
    { 
    Right,
    Left, 
        Up,//‘½•ªŽg‚í‚È‚¢
        Down,//‘½•ªŽg‚í‚È‚¢

    }
    public Movemode movemode = Movemode.Left;
    // Start is called before the first frame update
    void Start()
    {
        SavePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, Speed, 0);
        switch (movemode) 
        { 
            case Movemode.Left:
                if (SavePos.x - MoveArea < transform.position.x)
                {
                    transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
                }
                else 
                { 
                movemode = Movemode.Right;
                }
                break; 
            case Movemode.Right:
                if (SavePos.x + MoveArea > transform.position.x)
                {
                    transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
                }
                else
                {
                    movemode = Movemode.Left;
                }
                break;
        
        }
    }
}
