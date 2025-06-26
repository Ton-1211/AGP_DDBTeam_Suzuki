using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundScript : MonoBehaviour
{
    [SerializeField] AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        SR_SoundController soundController = FindAnyObjectByType<SR_SoundController>();
        source.volume = 1 * soundController.AllSeVolume;

        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
