using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenEffectScript : MonoBehaviour
{
    [Header("�X���[���[�V�������̃G�t�F�N�g"), SerializeField] Image slowEffect;

    void Update()
    {
        if(!slowEffect.enabled && PauseManager.IsSlow)// �X���[���n�܂����Ƃ�
        {
            slowEffect.enabled = true;
        }
        if(slowEffect.enabled && !PauseManager.IsSlow)// �X���[���I������Ƃ�
        {
            slowEffect.enabled = false;
        }
    }
}
