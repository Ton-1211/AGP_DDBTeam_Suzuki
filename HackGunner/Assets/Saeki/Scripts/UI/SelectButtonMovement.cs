using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class SelectButtonMovement : SelectButtonWindow
{
    [SerializeField] RectTransform[] ButtonTransforms;
    [SerializeField] float movementValue = 200f;

    public override void PushOnlyButtonEvent(int buttonPos)
    {
        if (buttonPos < 0 || buttonPos >= Buttons.Length) return;
        if (buttonPos == currentButtonIndex) return;

        audioSE?.PlayOneShot(audioSE.clip);

        if (currentButtonIndex == buttonPos)
        {
            ButtonInvoke();
            return;
        }
        ButtonMovememts(buttonPos - currentButtonIndex);
        currentButtonIndex = buttonPos;
        cursol.transform.localPosition = Buttons[currentButtonIndex].transform.localPosition;
    }
    protected override void SetNextSelect(int next)
    {
        if (next == 0) return;
        currentButtonIndex += next;
        buttonType = next < 0 ? ButtonType.Up : ButtonType.Down;
        ButtonMovememts(next);
        cursol.transform.localPosition = Buttons[currentButtonIndex].transform.localPosition;
        audioSE?.PlayOneShot(audioSE.clip);
    }

    void ButtonMovememts(int count)
    {
        Vector3 movement = AddPositions(count);

        for(int i = 0; i < ButtonTransforms.Length; i++)
        {
            ButtonTransforms[i].localPosition += movement;
        }
    }

    Vector3 AddPositions(int count)
    {
        Vector3 add = Vector3.one * movementValue * count;
        add.z = 0f; 
        if (input == input_or.Vertical)
        {
            add.x = 0f;
        }
        else
        {
            add.y = 0f;
        }

        
        add *= -1;
        
        return add;
    }
}
