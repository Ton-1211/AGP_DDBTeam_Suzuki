using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SR_SoundController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject SoundPrefab;

    public float AllSeVolume = 1;// 将来的に設定で音量を設定し、Start時にJSON等を読み込んで設定する

    public static SR_SoundController instance;

    void Start()
    {
        if (instance = null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 指定したオーディオクリップを一度だけ鳴らす
    /// </summary>
    public void PlaySEOnce(AudioClip Clip, Transform PlayPositionTransform = null)
    {
        GameObject CL_SoundPrefab = GameObject.Instantiate(SoundPrefab);
        if(PlayPositionTransform != null)
        {
            CL_SoundPrefab.transform.position = PlayPositionTransform.position;
        }
        SR_SoundPlay CL_SR_SoundPlay = CL_SoundPrefab.GetComponent<SR_SoundPlay>();
        CL_SR_SoundPlay.Clip = Clip;
        CL_SR_SoundPlay.Volume = 1 * AllSeVolume;
    }
}
