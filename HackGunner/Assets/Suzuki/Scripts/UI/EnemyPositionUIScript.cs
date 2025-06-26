using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPositionUIScript : MonoBehaviour
{
    [SerializeField] List<RectTransform> enemyIcons;
    [SerializeField] List<CharacterStatus> enemies;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < enemyIcons.Count; i++)
        {
            enemyIcons[i].position = RectTransformUtility.WorldToScreenPoint(UnityEngine.Camera.main, enemies[i].transform.position);
        }
    }
}
