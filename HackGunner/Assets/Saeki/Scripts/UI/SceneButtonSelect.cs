using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonSelect : MonoBehaviour
{
    [SerializeField] private AudioSource audioSE;

    public void SceneChangeTitle()
    {
        audioSE.PlayOneShot(audioSE.clip);
        SceneManager.LoadSceneAsync("Title");
    }

    public void SceneChangeMainGame(string SceneName)
    {
        audioSE.PlayOneShot(audioSE.clip);
        SceneManager.LoadSceneAsync(SceneName);
    }
    public void SceneChangeMainGameNoSE(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void SceneRerode()
    {
        audioSE.PlayOneShot(audioSE.clip);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void FinishGame()
    {
        audioSE.PlayOneShot(audioSE.clip);
        Application.Quit();
    }
}
