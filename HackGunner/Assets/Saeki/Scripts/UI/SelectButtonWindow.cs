using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class SelectButtonWindow : MonoBehaviour
{
    [SerializeField] protected Button[] Buttons;
    [SerializeField] protected GameObject cursol;
    [SerializeField] protected AudioSource audioSE;
    [SerializeField] protected input_or input = input_or.Vertical;
    [SerializeField] protected int currentButtonIndex = 0;
    [SerializeField] float InputResponseValue = 0.3f;
    [SerializeField] float InputRetryValue = 0.1f;
    protected enum ButtonType
    {
        Normal,
        Up,
        Down
    }
    protected enum input_or
    {
        Vertical,
        Horizontal
    }

    protected ButtonType buttonType = ButtonType.Normal;
   

    public virtual void PushOnlyButtonEvent(int buttonPos)
    {
        if (buttonPos < 0 || buttonPos >= Buttons.Length) return;
        if (buttonPos == currentButtonIndex) return;

        audioSE?.PlayOneShot(audioSE.clip);

        if (currentButtonIndex == buttonPos)
        {
            ButtonInvoke();
                return;
        }

        currentButtonIndex = buttonPos;
        cursol.transform.localPosition = Buttons[currentButtonIndex].transform.localPosition;
    }


    // Update is called once per frame
    void Update()
    {
        float inputValue = GetInputAxis();Debug.Log(inputValue);
        int NextSelect = GetNextSelect(inputValue);
        SetNextSelect(NextSelect);
        buttonType = ResetButtonType(inputValue);
        EnterButton();
    }

    float GetInputAxis()
    {
        return input == input_or.Vertical ? Input.GetAxisRaw("Vertical") : -Input.GetAxisRaw("Horizontal");
    }

    int GetNextSelect(float value)
    {
        if (Math.Abs(value) < InputResponseValue) return 0;
        if (buttonType != ButtonType.Normal) return 0;

        int NextButtonIndex = value < 0 ? 1 : -1;

        if (NextButtonIndex + currentButtonIndex >= Buttons.Length ||
           NextButtonIndex + currentButtonIndex < 0) return 0;

        return NextButtonIndex;

    }

    protected virtual void SetNextSelect(int next)
    {
        if (next == 0) return;
        currentButtonIndex += next;
        buttonType = next < 0 ? ButtonType.Up : ButtonType.Down;
        cursol.transform.localPosition = Buttons[currentButtonIndex].transform.localPosition;
        audioSE?.PlayOneShot(audioSE.clip);
    }

    ButtonType ResetButtonType(float value)
    {
        if (Math.Abs(value) < InputRetryValue)
            if (buttonType != ButtonType.Normal)
                return ButtonType.Normal;

        return buttonType;
    }

    void EnterButton()
    {
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown("space"))
        {
            ButtonInvoke();
        }
    }

    protected void ButtonInvoke()
    {
        Buttons[currentButtonIndex].onClick.Invoke();
    }
}
