using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceChangeScript : MonoBehaviour
{
    // 敵のデータでマテリアルを設定するかも、ゆくゆくはアニメーションを取ってくる予定だった
    [SerializeField] Material possessableMaterial;
    [SerializeField] Material deadMaterial;
    
    public void ChangeMaterialToPossessable(GameObject gameObject)// 乗り移り可能状態の色への設定
    {
        gameObject.GetComponent<MeshRenderer>().material = possessableMaterial;
    }
    public void ChangeMaterialToDead(GameObject gameObject)// 死亡状態の色への設定
    {
        gameObject.GetComponent<MeshRenderer>().material = deadMaterial;
    }
}
