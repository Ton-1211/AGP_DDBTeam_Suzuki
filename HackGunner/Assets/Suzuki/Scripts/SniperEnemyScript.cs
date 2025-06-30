using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemyScript : MonoBehaviour
{
    [Serializable]class LineWidth
    {
        [SerializeField] float min = 0.1f;
        [SerializeField] float max = 1f;
    }

    [SerializeField] LineWidth lineWidth;
    [SerializeField] AnimationCurve emitCurve;

    SearchColliderScript searchColliderScript;
    LineRenderer lineRenderer;
    float foundTimer = 0f;// プレイヤーを見つけている時間
    float emitTime = 0.05f;// 一回の点滅にかける時間
    bool firstFind = false;

    // Start is called before the first frame update
    void Start()
    {
        searchColliderScript = GetComponentInChildren<SearchColliderScript>();
        lineRenderer = GetComponent<LineRenderer>();
        firstFind = false;
    }

    void FixedUpdate()
    {
        if(searchColliderScript.IsFindPlayer && searchColliderScript.FoundPlayer != null)
        {
            foundTimer += Time.deltaTime;
            CreateLaser();
        }
        else if(firstFind)
        {
            OffLaser();
        }
    }

    void CreateLaser()
    {

        if(!firstFind)
        {
            firstFind = true;
            // ここにレーザーのシェーダーに再生開始処理を追加予定
        }

        Vector3 direction = searchColliderScript.FoundPlayer.position - transform.position;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + direction);
    }

    void OffLaser()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);

        firstFind = false;
    }

    IEnumerator Emit()
    {
        yield return null;
        // 急激な拡大、縮小をさせる予定
    }
}
