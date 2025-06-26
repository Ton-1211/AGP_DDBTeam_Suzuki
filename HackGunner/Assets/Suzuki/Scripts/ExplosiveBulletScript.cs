using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBulletScript : BulletBaseClass
{
    [Header("�������a"), SerializeField] float explosionRadius;
    [Header("�����̃G�t�F�N�g"), SerializeField] GameObject effect;
    void OnDestroy()
    {
        // �e�ۂ��j�󂳂��i�X�e�[�W���G�ɏՓ˂����j�Ƃ��ɔ���
        DamageExplosion();
        GameObject effectObject = Instantiate(effect, transform.position, Quaternion.identity);
    }

    void DamageExplosion()
    {
        // �v���C���[�������Ɋ������܂ꂽ�Ƃ�
        if ((transform.position - TargetManeger.getPlayerObj().transform.position).sqrMagnitude <= explosionRadius * explosionRadius)
        {
            TargetManeger.getPlayerObj().GetComponent<CharacterStatus>().TakeDamage(1f);
        }
        // �G�������Ɋ������܂ꂽ�Ƃ�
        List<EnemyBaseClass> hitEnemies = TargetManeger.TakeTarget(transform.position, explosionRadius);
        foreach(EnemyBaseClass enemy in hitEnemies)
        {
            enemy.TakeDamage(9999f, true);
        }
    }
}
