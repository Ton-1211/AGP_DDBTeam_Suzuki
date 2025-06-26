using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private bool isCursorOn = false;
    // Start is called before the first frame update
    void Start()
    {
        CursorSwich(isCursorOn);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CursorSwich(isCursorOn);
        }
    }

    void CursorSwich(bool isCursor)
    {
        if (isCursor)     
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isCursorOn = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isCursorOn = true;
        }
    }
}
