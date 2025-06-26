using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    Vector2 inputR, input;
    float moveZ;
    float moveX;

    float moveSpeed = 6f; //ˆÚ“®‘¬“x

    Vector3 velocity = Vector3.zero;@//ˆÚ“®•ûŒü
    Vector3 startpos;   //ŠJŽnˆÊ’u
    Vector3 rotation;   //‰ñ“]
    Vector3 rotaMemory; //‰ñ“]‹L‰¯

    float rotaSpeedX = 0.5f;   //x‰ñ“]‘¬“x
    float rotaSpeedY = 0.5f;   //y‰ñ“]‘¬“x


    float upLim = 315f;   //ãŠp“xãŒÀ
    float downLim = 45f;   //‰ºŠp“xãŒÀ



    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        // ˆÚ“®Ý’è

        rotation = transform.eulerAngles;
        rotaMemory = transform.eulerAngles;
        rotation.x = 0f;
        rotation.z = 0f;

        transform.eulerAngles = rotation;

        //‘OŒãˆÚ“®
        moveZ = input.y;
        //¶‰EˆÚ“®
        moveX = input.x;

        velocity = new Vector3(moveX, 0, moveZ).normalized * moveSpeed * Time.deltaTime;
        this.transform.Translate(velocity.x, velocity.y, velocity.z);

        transform.eulerAngles = rotaMemory;

        inputR.x += inputR.x * rotaSpeedX * Time.deltaTime;
        inputR.y += inputR.y * rotaSpeedY * Time.deltaTime;

        if (inputR.x > 1) { inputR.x = 1; }
        if (inputR.y > 1) { inputR.y = 1; }
        //Debug.Log(inputR.x+":"+inputR.y);

        transform.Rotate(-inputR.y, inputR.x, 0);

        rotation = transform.eulerAngles;

        if (rotation.x > downLim)
        {
            if (rotation.x > 180f)
            {
                if (upLim > rotation.x)
                {
                    rotation.x = upLim;
                }
            }
            else
            {
                rotation.x = downLim;
            }
        }

        rotation.z = 0f;

        transform.eulerAngles = rotation;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        //Debug.Log("Move");
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        inputR = context.ReadValue<Vector2>();
        //Debug.Log("Look");
    }
}
