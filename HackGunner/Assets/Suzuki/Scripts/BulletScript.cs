using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BulletBaseClass�Ɉڍs
/// </summary>
public class BulletScript : MonoBehaviour
{
    [SerializeField] BulletData bulletData;
    [Header("�e���Փ˂��郌�C���["), SerializeField] LayerMask layerMask;
    void Start()
    {
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerMask)
        {
            if (other.TryGetComponent<CharacterStatus>(out CharacterStatus character))// �L�����N�^�[�ɓ��������Ƃ�
            {
                if (character.ObjectTag == "Player")
                {
                    character.TakeDamage(1f);// �����_���[�W�킲�ƂŃ_���[�W�̒l��ς��邩��
                }
                else
                {
                    character.TakeDamage(bulletData.AttackPower);
                }
                Destroy(this.gameObject);
            }
        }
    }
}
