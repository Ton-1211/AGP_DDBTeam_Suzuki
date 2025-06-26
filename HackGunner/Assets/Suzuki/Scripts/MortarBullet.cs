using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBullet : MonoBehaviour
{
    [SerializeField] float flightTime = 1f;
    [Header("�Đ����x"), SerializeField] float speedRate = 2f;
    [SerializeField] BulletData bulletData;
    [Header("�����v���n�u"), SerializeField] GameObject explosionPrefab;
    [Header("���Д͈̓v���n�u"), SerializeField] GameObject warningAreaPrefab;
    [Header("�e���Փ˂��郌�C���["), SerializeField] LayerMask hitLayerMask;

    GameObject player;
    PlayerRay playerRay;
    Rigidbody rb;
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (!CompareTag("PlayerBullet"))
        {
            player = GameObject.FindWithTag("Player");
            targetPosition = player.transform.position;
        }
        else
        {
            playerRay = FindObjectOfType<PlayerRay>();
            targetPosition = playerRay.RayHitPosition;
        }
        rb = GetComponent<Rigidbody>();
        //Shoot();
        StartCoroutine(MoveParabolically());
    }

    void OnTriggerEnter(Collider other)
    {
        if(CompareLayer(hitLayerMask, other.gameObject.layer))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);// �����̐����i�G�t�F�N�g�ANavMesh Obstacle�n���j
            Destroy(gameObject);
        }
    }

    // �Փ˂���Layer��LayerMask�Ɋ܂܂�Ă��邩�m�F
    bool CompareLayer(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    /// <summary>
    /// �Ε��ړ����s���R���[�`��
    /// </summary>
    IEnumerator MoveParabolically()
    {
        GameObject warningArea = Instantiate(warningAreaPrefab, targetPosition, Quaternion.Euler(90f, 0f, 0f));
        Vector3 startPosition = transform.position;
        float differenceY = (targetPosition - startPosition).y;
        float initialVelocityVertical = (differenceY - Physics.gravity.y * 0.5f * flightTime * flightTime) / flightTime;

        for(float time = 0f; time < flightTime; time += (Time.deltaTime * speedRate))
        {
            Vector3 position = Vector3.Lerp(startPosition, targetPosition, time / flightTime);// x,z���ʏ�̍��W
            position.y = startPosition.y + initialVelocityVertical * time + 0.5f * Physics.gravity.y * time *time;// y���W
            transform.position = position;
            yield return null;
        }
        Destroy(warningArea);// ���Д͈͂��폜
        transform.position = targetPosition;// �Y���Ă����ꍇ�ɔ����ďC��
    }

    //void Shoot()
    //{
    //    float initialSpeed = CalcInitialSpeedFromAngle(targetPosition, angle);
    //    if (initialSpeed <= 0f)
    //    {
    //        Debug.LogWarning("���e�s�\�Ȓn�_���w�肵��܂���");
    //        return;
    //    }
    //    Vector3 shootVector = GetSpeedVector(initialSpeed, angle, targetPosition);
    //    Vector3 force = shootVector * rb.mass;
    //    rb.AddForce(force, ForceMode.Impulse);
    //}

    ///// <summary>
    ///// �p�x���珉���x�����߂�
    ///// </summary>
    //float CalcInitialSpeedFromAngle(Vector3 targetPosition, float angle)
    //{
    //    // xz���ʂ̋������v�Z
    //    Vector2 startPosPlane = new Vector2(transform.position.x, transform.position.z);
    //    Vector2 targetPosPlane = new Vector2(targetPosition.x, targetPosition.z);
    //    float distance = Vector2.Distance(targetPosPlane, startPosPlane);

    //    float x = distance;
    //    float g = Physics.gravity.y;
    //    float startY = transform.position.y;
    //    float targetY = targetPosition.y;

    //    float theta = angle * Mathf.Deg2Rad;// ���W�A���ɕϊ�
    //    float cosTheta = Mathf.Cos(theta);
    //    float tanTheta = Mathf.Tan(theta);

    //    float initialSpeedSquare = g * x * x / (2 * cosTheta * cosTheta * (targetY - startY - x * tanTheta));// �����x�̂Q��
    //    float initialSpeed;
    //    if (initialSpeedSquare <= 0)// �����x�̂Q�悪���̐��̏ꍇ�͋����ɂȂ��Ă��܂��̂Ōv�Z���I��
    //    {
    //        initialSpeed = 0f;
    //        return initialSpeed;
    //    }
    //    initialSpeed = Mathf.Sqrt(initialSpeedSquare);
    //    return initialSpeed;
    //}

    ///// <summary>
    ///// �����x���瑬�x�x�N�g���֕ϊ�
    ///// </summary>
    //Vector3 GetSpeedVector(float initialSpeed, float angle, Vector3 targetPosition)
    //{
    //    // xz���ʏ�̌v�Z
    //    Vector3 startPos = transform.position;
    //    Vector3 targetPos = targetPosition;
    //    startPos.y = 0f;
    //    targetPos.y = 0f;

    //    Vector3 direction = (targetPos - startPos).normalized;
    //    Quaternion yawRotation = Quaternion.FromToRotation(Vector3.right, direction);// ���[�̉�]
    //    Vector3 vector = initialSpeed * Vector3.right;

    //    vector = yawRotation * Quaternion.AngleAxis(angle, Vector3.forward) * vector;
    //    return vector;
    //}
}
