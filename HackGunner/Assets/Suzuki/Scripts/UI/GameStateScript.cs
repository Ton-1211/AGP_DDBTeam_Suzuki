using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameStateScript : MonoBehaviour
{
    public enum GameState
    {
        playing,
        Clear
    }

    [Header("クリア画面"), SerializeField] CanvasGroup clearCanvasGroup;
    [Header("クリア画面表示までの時間"),SerializeField] float waitTime;
    [SerializeField] float panelRiseTime;
    [Header("クリア画面が上がる量"), SerializeField] float panelRiseHeight;

    RectTransform rectTransform;
    [SerializeField]GameState gameState;
    float time;
    bool clear;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.playing;
        clear = false;
        time = 0f;

        if(clearCanvasGroup != null)
        {
            clearCanvasGroup.gameObject.TryGetComponent<RectTransform>(out rectTransform);
        }
    }

    void FixedUpdate()
    {
        UpdateByState();
    }

    public void ChangeGameState(GameState state)
    {
        gameState = state;
    }

    void UpdateByState()// ゲームの状態ごとのUpdate
    {
        switch(gameState)
        {
            case GameState.playing:
                time += Time.deltaTime;
                break;
            case GameState.Clear:
                if(!clear)
                {
                    StartCoroutine(ClearProcess());
                    clear = true;
                }
                break;
            default:
                break;
        }
    }

    IEnumerator ClearProcess()// クリア処理
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Time.timeScale = 0f;
        float clearTime = time;

        clearCanvasGroup.gameObject.SetActive(true);
        yield return RisePanels(clearCanvasGroup);
    }

    IEnumerator RisePanels(CanvasGroup canvasGroup)// クリア画面を上げる
    {
        float riseRate = panelRiseHeight / panelRiseTime;
        float timer = panelRiseTime;
        while(timer > 0f)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + riseRate * Time.unscaledDeltaTime);
            timer -= Time.unscaledDeltaTime;
            yield return null;
        }
    }
}
