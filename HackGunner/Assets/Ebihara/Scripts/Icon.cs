using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{
    CharacterStatus characterStatus;

    // Start is called before the first frame update
    void Start()
    {
        characterStatus = transform.parent.GetComponent<CharacterStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterStatus != null && characterStatus.IsDead == true)
        {
            this.gameObject.SetActive(false);
        }
    }
}
