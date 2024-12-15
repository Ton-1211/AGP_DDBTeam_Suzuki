using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Explosion : CharacterStatus
{
    float destroyTimer = 1f;
    bool isExplosion;
    string pBullet = "PlayerBullet";
    string enemyTag = "Enemy";
    string playerTag = "Player";
    [SerializeField] float explosiveRadius;
    [SerializeField] GameObject explosionprefab;

    // Start is called before the first frame update
    void Start()
    {
        isExplosion = false;
        StartSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDead)
        {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (isExplosion == false && other.CompareTag(pBullet) == true)
        {
            isExplosion = true;
            GameObject explosionObj = Instantiate(explosionprefab, transform.position, Quaternion.identity);
            Destroy(gameObject);

            RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, explosiveRadius, Vector3.forward, 0.0f);

            //Debug.Log("���o���ꂽ�R���C�_�[�̐�:" + hits.Length);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.TryGetComponent<CharacterStatus>(out CharacterStatus status))
                {
                    if (hits[i].collider.CompareTag(enemyTag) == true)
                    {
                        Debug.Log("�G���o:" + hits[i].collider.name);
                        status.TakeDamage(9999f, false);
                        if (hits[i].collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
                        {
                            rb.AddForce(Vector3.up * 13f, ForceMode.Impulse);
                        }
                    }
                    else if (hits[i].collider.CompareTag(playerTag) == true)
                    {
                        Debug.Log("�v���C���[���o:" + hits[i].collider.name);
                        status.TakeDamage(1f);
                    }
                }
                //Debug.Log("���o���ꂽ�I�u�W�F�N�g:" + hits[i].collider.name);
            }
            Destroy(explosionObj, destroyTimer);
        }
    }

    void Explode()
    {
        if (isExplosion == false)
        {
            isExplosion = true;
            GameObject explosionObj = Instantiate(explosionprefab, transform.position, Quaternion.identity);
            Destroy(gameObject);

            RaycastHit[] hits = Physics.SphereCastAll(this.transform.position, explosiveRadius, Vector3.forward, 0.0f);

            //Debug.Log("���o���ꂽ�R���C�_�[�̐�:" + hits.Length);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.TryGetComponent<CharacterStatus>(out CharacterStatus status))
                {
                    if (hits[i].collider.CompareTag(enemyTag) == true)
                    {
                        Debug.Log("�G���o:" + hits[i].collider.name);
                        status.TakeDamage(9999f, true);
                        //if (hits[i].collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
                        //{
                        //    rb.AddForce(Vector3.up * 13f, ForceMode.Impulse);
                        //}
                    }
                    else if (hits[i].collider.CompareTag(playerTag) == true)
                    {
                        Debug.Log("�v���C���[���o:" + hits[i].collider.name);
                        status.TakeDamage(1f);
                    }
                }
                //Debug.Log("���o���ꂽ�I�u�W�F�N�g:" + hits[i].collider.name);
            }
            Destroy(explosionObj, destroyTimer);
        }
    }
}
