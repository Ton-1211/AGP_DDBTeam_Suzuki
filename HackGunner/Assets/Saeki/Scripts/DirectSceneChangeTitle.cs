using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectSceneChangeTitle : MonoBehaviour
{
    bool InputCheck = false;

    void Start()
    {
        InputCheck = false;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) && Input.GetKey(KeyCode.Alpha2))
        {
            SceneChangeTitle();
        }
    }

    void SceneChangeTitle()
    {
        if (InputCheck == false)
            SceneManager.LoadSceneAsync("Title");

        InputCheck = true;
    }
}
