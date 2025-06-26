using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunUIScript : MonoBehaviour
{
    [Tooltip("�c�e���̃e�L�X�g"),SerializeField] TMP_Text remainBulletText;
    [Header("�e�̃A�C�R��"), SerializeField] Image gunIcon;
    [SerializeField] PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        remainBulletText.SetText("Remain:{0}", player.Gun.RemainBullets);// �c�e���̕\��
        if(player.Gun.RemainBullets == 0)// �c�e��0�̂Ƃ�
        {
            remainBulletText.color = Color.red;
        }
        else if(remainBulletText.color == Color.red)// �c�e��0����񕜂����Ƃ��i����؂�ւ�������ڂ莞�j
        {
            remainBulletText.color = Color.white;
        }

        if (gunIcon != null)
        {
            gunIcon.sprite = player.Gun.WeaponImage;
        }
    }
}
