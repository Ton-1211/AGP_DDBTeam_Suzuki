using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_StartBGM : MonoBehaviour
{
    [SerializeField]AudioSource audio;
    float a;
    SR_StartBGM startBGM;

    // Start is called before the first frame update
    void Start()
    {
        startBGM = GetComponent<SR_StartBGM>();
    }

    // Update is called once per frame
    void Update()
    {
        a = Time.timeScale;
        if (a > 0) 
        { 
        audio.Play();
            Destroy(startBGM);
        }
    }
}
