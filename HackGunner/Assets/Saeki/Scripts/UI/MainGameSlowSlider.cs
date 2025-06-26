using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MainGameSrowSlider : MonoBehaviour
{
    [SerializeField] private Image SliderUIImage;
    [SerializeField] private RectTransform SliderValueTransform;
    [SerializeField] private Animator animator;
    // Update is called once per frame
    void FixedUpdate()
    {
        float SlowValue = TargetManeger.GetSlowValue();
        //Debug.Log(SlowValue);

        animator.SetBool("SliderDirected", SlowValue != 0f);

        SetSliderValue(SlowValue);
        SetSliderColor(SlowValue);
    }

    private void SetSliderColor(float Value)
    {
        Color ValueColor = Color.black;
        ValueColor.r = Value;
        ValueColor.g = 1f - Value;
        SliderUIImage.color = ValueColor;
    }

    private void SetSliderValue(float Value)
    {
        Vector3 TransformScale = Vector3.one;

        if(1f - Value > 0f)
        {
            TransformScale.x = 1f - Value;
        }
        else
        {
            TransformScale.x = 0f;
            animator.SetBool("SliderDirected", false);
        }
       
        SliderValueTransform.localScale = TransformScale;
    }
}
