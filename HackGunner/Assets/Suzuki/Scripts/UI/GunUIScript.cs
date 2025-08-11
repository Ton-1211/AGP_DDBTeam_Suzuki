using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunUIScript : MonoBehaviour
{
    [Tooltip("残弾数のテキスト"),SerializeField] TMP_Text remainBulletText;
    [Header("銃のアイコン"), SerializeField] Image gunIcon;
    [SerializeField] PlayerMove player;

    void Update()
    {
        // 残弾数の表示
        remainBulletText.SetText("Remain:{0}", player.Gun.RemainBullets);

        // 残弾が0のとき
        if (player.Gun.RemainBullets == 0)
        {
            remainBulletText.color = Color.red;
        }

        // 残弾が0から回復したとき（武器切り替え時や乗り移り時）
        else if (remainBulletText.color == Color.red)
        {
            remainBulletText.color = Color.white;
        }

        // 銃のアイコンを表示
        if (gunIcon != null)
        {
            gunIcon.sprite = player.Gun.WeaponImage;
        }
    }
}
