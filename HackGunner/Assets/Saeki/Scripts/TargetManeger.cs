using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TargetManeger : MonoBehaviour
{
    private static List<EnemyBaseClass> Enemy;
    private static GameObject playerObject;
    private static CharacterStatus playerStatus;
    private static float TimeCount = 0;

    [SerializeField] private static float Interval = 4.5f;// スロー時間
    [SerializeField] private static float watchDistancs = 15f;



    public static List<EnemyBaseClass > EnemyList => Enemy;

    /// <summary>
    /// staticで宣言されたGetメゾット
    /// </summary>
    /// <returns>プレイヤーのオブジェクト</returns>
    public static GameObject getPlayerObj() { return playerObject; }

    public static CharacterStatus PlayerStatus { get { return playerStatus; } }

    /// <summary>
    /// staticで宣言されたGetメゾット
    /// </summary>
    /// <returns>残りのSlow状態を(下限)0~1(上限)までに変換したパラメータ</returns>
    public static float GetSlowValue()
    {
        float value = TimeCount / Interval;
        if (value < 0f) value = 0f;
        else if (value > 1f) value = 1f;
        return value; 
    }
    
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        playerStatus = playerObject.GetComponent<CharacterStatus>();
        GameObject[] onFieldEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        TimeCount = 0;

        Enemy = new List<EnemyBaseClass>();
        foreach (GameObject g in onFieldEnemy)
            AddEnemyBaseClass(g);
    }

    private void Update()
    {
        TimeScaleManagement();
    }

    private void TimeScaleManagement()
    {
        if (Time.timeScale == 1f || Time.timeScale == 0f || Time.timeScale == 0.1f)
            return;

        TimeCount += Time.unscaledDeltaTime;

        if (TimeCount > Interval)
            Time.timeScale = 1f;
    }

    /// <summary>
    /// 乗り移りの頭を投げる状態に一時的にtimeScaleを戻す
    /// </summary>
    public static void StartHeadChange()
    {
        TimeCount = 0;
        Time.timeScale = 0.1f;
    }

    /// <summary>
    /// 敵対対象の引数の確保
    /// </summary>
    public static void AddEnemyBaseClass(GameObject enemy)
    {
        Enemy.Add(enemy.GetComponentInParent<EnemyBaseClass>());
    }
    /// <summary>
    /// 敵対する対象を変更する
    /// </summary>
    public static void SetTarget(GameObject player)
    {
        playerObject = player;
        playerStatus = playerObject.GetComponent<CharacterStatus>();
        TimeCount = 0;
        Time.timeScale = 0.5f;
        ChangeTarget();
    }
   
    private static void ChangeTarget()
    {
        foreach (EnemyBaseClass baseClass in Enemy)
        {
            baseClass.ChangeTarget(playerObject);
        }
    }

    /// <summary>
    /// 一定の距離にあるEnemyのターゲット発見させる
    /// </summary>
    public static void WatchTarget()
    {
        foreach (EnemyBaseClass baseClass in Enemy)
        {
            if (distance_Square(playerObject.transform.position,baseClass.transform.position) < watchDistancs)    
                baseClass.Watch();
        }
    }
    /// <summary>
    /// ある位置座標から一定の距離にあるEnemyを取得
    /// </summary>
    /// <param name="position">位置座標</param>
    /// <param name="radius">位置座標からの距離</param>
    /// <returns>一定の距離内にあるEnemyのList</returns>
    public static List<EnemyBaseClass> TakeTarget(Vector3 position, float radius)
    {
        List<EnemyBaseClass> list = new();
        foreach (EnemyBaseClass baseClass in Enemy)
        {
            if (distance_Square(position ,baseClass.transform.position) < radius * radius)
                list.Add(baseClass);
        }
        return list;
    }


    private static float distance_Square(Vector3 Target ,Vector3 enemy) 
    { 
        return (Target - enemy).sqrMagnitude; 
    }
}