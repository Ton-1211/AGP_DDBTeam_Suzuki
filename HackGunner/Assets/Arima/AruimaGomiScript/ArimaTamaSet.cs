using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArimaTamaSet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            Debug.Log("AAAA");
            transform.position = new Vector3(transform.position.x, transform.position.y+12, transform.position.z) ;
        
        }
    }
}
