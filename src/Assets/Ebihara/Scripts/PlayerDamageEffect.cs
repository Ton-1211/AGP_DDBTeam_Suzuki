using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageEffect : MonoBehaviour
{
    [SerializeField] int nowHP;
    const int maxHP = 3;
    Change change;

    [SerializeField] Image damageImage;
    [SerializeField] Image[] hpsGreen = new Image[maxHP];

    float[] damagesAlpha;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        change= GameObject.FindObjectOfType<Change>();
        nowHP = maxHP;
        color = damageImage.GetComponent<Image>().color;
        color.a = 0f;
        damageImage.GetComponent<Image>().color = color;
        damagesAlpha = new float[maxHP + 1];
        for (int i = maxHP; i > 0; i--)
        {
            damagesAlpha[maxHP - i] = 1.0f - 0.3f * (maxHP - i);
        }
        damagesAlpha[maxHP] = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageEffect(float hp)
    {
        if (change.Changing == false)
        {
            for (int i = 0; i < maxHP; i++)
            {
                if (i < hp)
                {
                    hpsGreen[i].enabled = true;
                }
                else
                {
                    hpsGreen[i].enabled = false;
                }
            }
            nowHP = (int)hp;
            color.a = damagesAlpha[nowHP];
            damageImage.GetComponent<Image>().color = color;

            //Debug.Log("Hit:" + nowHP);
        }
    }
    public void Reset()
    {
        nowHP = maxHP;
        color.a = damagesAlpha[maxHP];
        damageImage.GetComponent<Image>().color = color;
        for (int i = 0; i < hpsGreen.Length; i++)
        {
            hpsGreen[i].enabled = true;
        }
    }
}
