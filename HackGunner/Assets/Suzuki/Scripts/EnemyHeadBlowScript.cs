using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadBlowScript : MonoBehaviour
{
    [SerializeField] float power;
    [SerializeField] Rigidbody rigidbody;
    [Header("頭が消えるまでの時間"), SerializeField] float destroySeconds = 1f;

    /// <summary>
    /// 頭を吹き飛ばす
    /// </summary>
    /// <param name="basePosition">吹き飛ばした原因の位置</param>
    public void BlowOff(Vector3 basePosition)
    {
        // 方向を計算
        Vector3 direction = transform.position - basePosition;
        direction.Normalize();

        rigidbody.AddForce(direction * power, ForceMode.Impulse);// 頭を吹き飛ばす
        StartCoroutine(DestroyAfterSeconds(destroySeconds));// 一定時間後に消去
    }

    IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
