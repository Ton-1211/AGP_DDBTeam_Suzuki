using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffectScript : MonoBehaviour
{
    [Header("スローモーション中のエフェクト"), SerializeField] Image slowEffect;

    void Update()
    {
        if(!slowEffect.enabled && PauseManager.IsSlow)// スローが始まったとき
        {
            slowEffect.enabled = true;// エフェクトを有効化
        }
        if(slowEffect.enabled && !PauseManager.IsSlow)// スローが終わったとき
        {
            slowEffect.enabled = false;// エフェクトを無効化
        }
    }
}
