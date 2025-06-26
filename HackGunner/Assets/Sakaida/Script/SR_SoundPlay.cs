using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_SoundPlay : MonoBehaviour
{
    public AudioClip Clip;

    AudioSource source;

    bool one = false;
    public float Volume = 0;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!one)
        {
            source.clip = Clip;
            source.Play();
            one = true;
        }
        else 
        {
            if (!source.isPlaying) 
            {
                Destroy(gameObject);
            }
        }

    }
}
