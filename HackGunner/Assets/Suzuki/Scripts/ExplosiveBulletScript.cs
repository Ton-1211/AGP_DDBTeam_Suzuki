using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBulletScript : BulletBaseClass
{
    [Header("爆発半径"), SerializeField] float explosionRadius;
    [Header("爆発のエフェクト"), SerializeField] GameObject effect;
    void OnDestroy()
    {
        // 弾丸が破壊される（ステージか敵に衝突した）ときに爆発
        DamageExplosion();
        GameObject effectObject = Instantiate(effect, transform.position, Quaternion.identity);
    }

    void DamageExplosion()
    {
        // プレイヤーが爆風に巻き込まれたとき
        if ((transform.position - TargetManeger.getPlayerObj().transform.position).sqrMagnitude <= explosionRadius * explosionRadius)
        {
            TargetManeger.getPlayerObj().GetComponent<CharacterStatus>().TakeDamage(1f);
        }
        // 敵が爆風に巻き込まれたとき
        List<EnemyBaseClass> hitEnemies = TargetManeger.TakeTarget(transform.position, explosionRadius);
        foreach(EnemyBaseClass enemy in hitEnemies)
        {
            enemy.TakeDamage(9999f, true);
        }
    }
}
