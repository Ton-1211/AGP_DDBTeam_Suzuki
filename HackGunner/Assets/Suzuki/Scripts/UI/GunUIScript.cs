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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        remainBulletText.SetText("Remain:{0}", player.Gun.RemainBullets);// 残弾数の表示
        if(player.Gun.RemainBullets == 0)// 残弾が0のとき
        {
            remainBulletText.color = Color.red;
        }
        else if(remainBulletText.color == Color.red)// 残弾が0から回復したとき（武器切り替え時や乗り移り時）
        {
            remainBulletText.color = Color.white;
        }

        if (gunIcon != null)
        {
            gunIcon.sprite = player.Gun.WeaponImage;
        }
    }
}
