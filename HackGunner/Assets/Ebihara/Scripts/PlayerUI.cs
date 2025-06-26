using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    Change change;
    [SerializeField] PlayerDamageEffect damageEffect;

    // Start is called before the first frame update
    void Start()
    {
        change = GameObject.FindObjectOfType<Change>();
    }

    // Update is called once per frame
    void Update()
    {
        damageEffect.DamageEffect(change.CharacterStatusHp);Debug.Log(change.CharacterStatusHp);
    }
}
