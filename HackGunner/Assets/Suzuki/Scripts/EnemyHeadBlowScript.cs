using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadBlowScript : MonoBehaviour
{
    [SerializeField] float power;
    [SerializeField] Rigidbody rigidbody;
    [Header("頭が消えるまでの時間"), SerializeField] float destroySeconds = 1f;

    public void BlowOff(Vector3 basePosition)
    {
        Vector3 direction = transform.position - basePosition;
        direction.Normalize();

        rigidbody.AddForce(direction * power, ForceMode.Impulse);
        StartCoroutine(DestroyAfterSeconds(destroySeconds));
    }

    IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
