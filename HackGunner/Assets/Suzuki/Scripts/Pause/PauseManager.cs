using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool IsSlow { get { return Time.timeScale < 1 && Time.timeScale > 0; } }
    public static bool IsPaused { get { return Time.timeScale == 0; } }

    void Update()
    {
        if(IsSlow && TargetManeger.PlayerStatus.CharacterAnimator.updateMode == AnimatorUpdateMode.Normal)// �X���[���Ƀv���C���[�̃A�j���[�V�����͕ς��Ȃ��悤��
        {
            TargetManeger.PlayerStatus.CharacterAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        if(!IsSlow && TargetManeger.PlayerStatus.CharacterAnimator.updateMode == AnimatorUpdateMode.UnscaledTime)// �X���[�������͒ʏ�ɖ߂��Ă���
        {
            TargetManeger.PlayerStatus.CharacterAnimator.updateMode = AnimatorUpdateMode.Normal;
        }

        if(IsPaused && TargetManeger.PlayerStatus.CharacterAnimator.updateMode == AnimatorUpdateMode.UnscaledTime)// �|�[�Y���͓������~�܂�悤��
        {
            TargetManeger.PlayerStatus.CharacterAnimator.updateMode = AnimatorUpdateMode.Normal;
        }
    }
}
