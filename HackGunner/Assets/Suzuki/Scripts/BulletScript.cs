using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BulletBaseClassに移行
/// </summary>
public class BulletScript : MonoBehaviour
{
    [SerializeField] BulletData bulletData;
    [Header("弾が衝突するレイヤー"), SerializeField] LayerMask layerMask;
    void Start()
    {
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerMask)
        {
            if (other.TryGetComponent<CharacterStatus>(out CharacterStatus character))// キャラクターに当たったとき
            {
                if (character.ObjectTag == "Player")
                {
                    character.TakeDamage(1f);// 将来ダメージ種ごとでダメージの値を変えるかも
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
