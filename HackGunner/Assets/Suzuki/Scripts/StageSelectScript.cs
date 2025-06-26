using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectScript : MonoBehaviour
{
    [Tooltip("�X�e�[�W�̔ԍ�"), SerializeField] StageNumber stageNumber;

    public void StartStage(int sceneNum, int stageNum)
    {
        stageNumber.Num = stageNum;
        SceneManager.LoadScene(sceneNum);
    }
}
