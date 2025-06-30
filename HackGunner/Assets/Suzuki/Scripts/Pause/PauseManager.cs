using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool IsSlow { get { return Time.timeScale < 1 && Time.timeScale > 0; } }
    public static bool IsPaused { get { return Time.timeScale == 0; } }

    void Update()
    {
        if(IsSlow && TargetManeger.PlayerStatus.CharacterAnimator.updateMode == AnimatorUpdateMode.Normal)// スロー時にプレイヤーのアニメーションは変わらないように
        {
            TargetManeger.PlayerStatus.CharacterAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        if(!IsSlow && TargetManeger.PlayerStatus.CharacterAnimator.updateMode == AnimatorUpdateMode.UnscaledTime)// スロー解除時は通常に戻しておく
        {
            TargetManeger.PlayerStatus.CharacterAnimator.updateMode = AnimatorUpdateMode.Normal;
        }

        if(IsPaused && TargetManeger.PlayerStatus.CharacterAnimator.updateMode == AnimatorUpdateMode.UnscaledTime)// ポーズ中は動きが止まるように
        {
            TargetManeger.PlayerStatus.CharacterAnimator.updateMode = AnimatorUpdateMode.Normal;
        }
    }
}
