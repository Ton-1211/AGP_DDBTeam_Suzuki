using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SR_GameStarMovieManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI MeireiText;
    [SerializeField] Image Logo;
    [SerializeField] AudioClip StartSE;
    [SerializeField] AudioClip EndSE;

    SR_SoundController SoundController => SR_SoundController.instance;

    public enum PlayMode 
    { 
    KillStage,
    LiveStage,
    GoalStage
    }
    public PlayMode playmode = PlayMode.KillStage;
    RectTransform MeireiRect;
    RectTransform LogoRect;
    float Timer = 0;
    float fontSize = 0;
    float LogoSize = 0;
    float RandomPosPP;

    bool StartSe= false;
    bool EndSe= false;
    // Start is called before the first frame update
    void Start()
    {
        MeireiRect = MeireiText.GetComponent<RectTransform>();
        LogoRect = Logo.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // 水平軸の入力を取得
        float horizontalInput = Input.GetAxis("Horizontal");

        // 垂直軸の入力を取得
        float verticalInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || verticalInput != 0) 
        { 
        gameObject.SetActive(false);
            if (!EndSe)
            {
                EndSe = true;
                SoundController.PlaySEOnce(EndSE);
            }
        }

        MeireiText.fontSize = fontSize;
        LogoRect.localScale = new Vector3(LogoSize,LogoSize);
        if (!StartSe) 
        {
            StartSe = true;
            SoundController.PlaySEOnce(StartSE);
        }
        
        Timer += Time.deltaTime;
        if (fontSize < 150)
        {
            fontSize = Timer * 550;
        }
        else if (fontSize < 160) 
        {
            fontSize = Timer * 200;

        }

        if (LogoSize < 7.5f) 
        {
            LogoSize = Timer * 40;
        }
        
        RandomPosPP = Random.Range(-2, 2);
        MeireiRect.anchoredPosition = new Vector2(RandomPosPP, RandomPosPP+ 124);
        //MeireiText.transform.position = new Vector3(RandomPosPP, RandomPosPP+50, 0);
        switch (playmode) 
        {
            case PlayMode.KillStage:
                MeireiText.SetText("全員始末しろ！");
                break;
            case PlayMode.LiveStage:
                MeireiText.SetText("一定時間生き残れ！");
                break;
            case PlayMode.GoalStage:
                MeireiText.SetText("目標地点を目指せ！");
                break;


        }    
    }

}
