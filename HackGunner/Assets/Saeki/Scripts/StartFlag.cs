using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class StartFlag : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] string animText;
    private bool startCheck;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        startCheck = false;
    }
   
    // Update is called once per frame
    void Update()
    {
        float inputVertical = Input.GetAxisRaw("Vertical");
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        bool MouseLeft = Input.GetMouseButton(0);

        if (!startCheck && (inputVertical != 0 || inputHorizontal != 0 || MouseLeft))
        {
            AnimationOnePlay();
            StartCounting();
            Time.timeScale = 1;           
        }
    }


    void AnimationOnePlay()
    {
        anim.Play(animText, 0, 1f);
    }

    async Task StartCounting()
    {
        if (startCheck)
        {
            Debug.LogWarning("���ɃJ�E���g���ł��I");
            return;
        }

        startCheck = true;
       
        float startTime = Time.realtimeSinceStartup;

        // �񓯊��őҋ@
        while (Time.realtimeSinceStartup - startTime < 0.2f)
        {
            await Task.Yield(); // �񓯊��Ŏ��̃t���[���܂őҋ@
        }

        this.gameObject.SetActive(false);
    }
}
