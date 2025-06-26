using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] Transform obj;
    [SerializeField] float speed;

    void Start()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;
    }
    void FixedUpdate()
    {
        obj.Rotate(Vector3.up * speed);
    }
}
