using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBullet : MonoBehaviour
{
    [SerializeField] float flightTime = 1f;
    [Header("再生速度"), SerializeField] float speedRate = 2f;
    [SerializeField] BulletData bulletData;
    [Header("爆風プレハブ"), SerializeField] GameObject explosionPrefab;
    [Header("脅威範囲プレハブ"), SerializeField] GameObject warningAreaPrefab;
    [Header("弾が衝突するレイヤー"), SerializeField] LayerMask hitLayerMask;

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
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);// 爆風の生成（エフェクト、NavMesh Obstacle系統）
            Destroy(gameObject);
        }
    }

    // 衝突したLayerがLayerMaskに含まれているか確認
    bool CompareLayer(LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    /// <summary>
    /// 斜方移動を行うコルーチン
    /// </summary>
    IEnumerator MoveParabolically()
    {
        GameObject warningArea = Instantiate(warningAreaPrefab, targetPosition, Quaternion.Euler(90f, 0f, 0f));
        Vector3 startPosition = transform.position;
        float differenceY = (targetPosition - startPosition).y;
        float initialVelocityVertical = (differenceY - Physics.gravity.y * 0.5f * flightTime * flightTime) / flightTime;

        for(float time = 0f; time < flightTime; time += (Time.deltaTime * speedRate))
        {
            Vector3 position = Vector3.Lerp(startPosition, targetPosition, time / flightTime);// x,z平面上の座標
            position.y = startPosition.y + initialVelocityVertical * time + 0.5f * Physics.gravity.y * time *time;// y座標
            transform.position = position;
            yield return null;
        }
        Destroy(warningArea);// 脅威範囲を削除
        transform.position = targetPosition;// ズレていた場合に備えて修正
    }

    //void Shoot()
    //{
    //    float initialSpeed = CalcInitialSpeedFromAngle(targetPosition, angle);
    //    if (initialSpeed <= 0f)
    //    {
    //        Debug.LogWarning("着弾不可能な地点が指定しれました");
    //        return;
    //    }
    //    Vector3 shootVector = GetSpeedVector(initialSpeed, angle, targetPosition);
    //    Vector3 force = shootVector * rb.mass;
    //    rb.AddForce(force, ForceMode.Impulse);
    //}

    ///// <summary>
    ///// 角度から初速度を求める
    ///// </summary>
    //float CalcInitialSpeedFromAngle(Vector3 targetPosition, float angle)
    //{
    //    // xz平面の距離を計算
    //    Vector2 startPosPlane = new Vector2(transform.position.x, transform.position.z);
    //    Vector2 targetPosPlane = new Vector2(targetPosition.x, targetPosition.z);
    //    float distance = Vector2.Distance(targetPosPlane, startPosPlane);

    //    float x = distance;
    //    float g = Physics.gravity.y;
    //    float startY = transform.position.y;
    //    float targetY = targetPosition.y;

    //    float theta = angle * Mathf.Deg2Rad;// ラジアンに変換
    //    float cosTheta = Mathf.Cos(theta);
    //    float tanTheta = Mathf.Tan(theta);

    //    float initialSpeedSquare = g * x * x / (2 * cosTheta * cosTheta * (targetY - startY - x * tanTheta));// 初速度の２乗
    //    float initialSpeed;
    //    if (initialSpeedSquare <= 0)// 初速度の２乗が負の数の場合は虚数になってしまうので計算を終了
    //    {
    //        initialSpeed = 0f;
    //        return initialSpeed;
    //    }
    //    initialSpeed = Mathf.Sqrt(initialSpeedSquare);
    //    return initialSpeed;
    //}

    ///// <summary>
    ///// 初速度から速度ベクトルへ変換
    ///// </summary>
    //Vector3 GetSpeedVector(float initialSpeed, float angle, Vector3 targetPosition)
    //{
    //    // xz平面上の計算
    //    Vector3 startPos = transform.position;
    //    Vector3 targetPos = targetPosition;
    //    startPos.y = 0f;
    //    targetPos.y = 0f;

    //    Vector3 direction = (targetPos - startPos).normalized;
    //    Quaternion yawRotation = Quaternion.FromToRotation(Vector3.right, direction);// ヨーの回転
    //    Vector3 vector = initialSpeed * Vector3.right;

    //    vector = yawRotation * Quaternion.AngleAxis(angle, Vector3.forward) * vector;
    //    return vector;
    //}
}
