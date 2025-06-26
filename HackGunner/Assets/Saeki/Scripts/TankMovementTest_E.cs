using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class TankMovementTest_E : EnemyBaseClass
{
    [SerializeField]
    GameObject TankBody, TankCanon, TankCatapera;
    [SerializeField] private float XRotationLimit = 30f; // X軸回転の最小角度
    private int cornersCount = 0;
    private Vector3 directionToNext;

    protected override Vector3 GetTargetPos() { return TargetSetting.transform.position; }
    void BodyRotete()
    {
        Vector3 directionToTarget = Target.transform.position - TankBody.transform.position;
        directionToTarget.y = 0; // 水平方向だけに向く場合、Y軸の回転を無視する
        LookSlerp(TankBody, directionToTarget);
    }
    void CataperaRotete()
    {
        var corners = Agent.path.corners;
        if (2 > corners.Length)
            return;

        directionToNext = corners[1] - corners[0];
        directionToNext.y = 0; // 水平方向だけに向く場合、Y軸の回転を無視する

        LookSlerp(TankCatapera, directionToNext);
    }
    void CanonRotete()
    {
        // キャノン砲からターゲットへのベクトルを計算
        Vector3 directionToTarget = Target.transform.position - TankCanon.transform.position;

        // Y軸とZ軸を無視し、X軸回転のみを計算
        float targetXRotation = Mathf.Atan2(directionToTarget.y, directionToTarget.z) * Mathf.Rad2Deg;

        // X軸の回転角度を-30から30度に制限
        targetXRotation = Mathf.Clamp(targetXRotation, -XRotationLimit, XRotationLimit);

        // 現在のキャノン砲の回転を取得し、X軸のみ変更
        Vector3 newRotation = new Vector3(-targetXRotation, TankCanon.transform.localEulerAngles.y, TankCanon.transform.localEulerAngles.z);

        // キャノン砲の回転を徐々に更新
        TankCanon.transform.localRotation = Quaternion.Slerp(TankCanon.transform.localRotation, Quaternion.Euler(newRotation), Time.deltaTime * rotationSpeed);

        return;


        directionToTarget.y = 0;
        directionToTarget.z = 0;

        float angle = Vector3.Angle(TankCanon.transform.forward, directionToTarget);

        // 一定以上の角度差があれば、ターゲットの方向を向く
        if (angle > 0.1f)
        {
            // ターゲット方向に向けた回転を計算
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            float xRotate = Mathf.Atan2(directionToTarget.y, directionToTarget.z) * Mathf.Rad2Deg;//targetRotation.eulerAngles.x;
            Vector3 newvec = new(xRotate, TankCanon.transform.eulerAngles.y, TankCanon.transform.eulerAngles.z);

            // 徐々にターゲットの方向に回転
            TankCanon.transform.localRotation = Quaternion.Lerp(TankCanon.transform.localRotation,
                Quaternion.Euler(newvec), rotationSpeed * Time.deltaTime);
        }
    }

    void LookSlerp(GameObject tankPart,Vector3 dir)
    {
        float angle = Vector3.Angle(tankPart.transform.forward, dir);

        // 一定以上の角度差があれば、ターゲットの方向を向く
        if (angle > 0.1f)
        {
            // ターゲット方向に向けた回転を計算
            Quaternion targetRotation = Quaternion.LookRotation(dir);

            // 徐々にターゲットの方向に回転
            tankPart.transform.rotation = Quaternion.Slerp(tankPart.transform.rotation,
                targetRotation, rotationSpeed * rotationSpeed * Time.deltaTime);
        }
    }
    protected override void StopChase()
    {
        CataperaRotete();
        BodyRotete();

        if (FindCheck())
        {
            Agent.speed = 0f;

            // ターゲットの方向への回転          
            CanonRotete();

            remainingCount += Time.deltaTime;

            if (lockonCount < lockonIntarval)
            {
                lockonCount += Time.deltaTime;
                lockonCheck = true;
            }
            else
            {
                lockonCheck = false;
            }
        }
        else
        {
            remainingCheck = false;
            remainingCount = 0f;
            lockonCount = 0f;
            Agent.speed = moveSpeed;
        }
    }
}
