using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButtonBetween : SelectButtonWindow
{
    [SerializeField] RectTransform LeftIcon;
    [SerializeField] RectTransform RightIcon;
    [SerializeField] float IconAdjustmentValue = 20f;
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

        currentButtonIndex = buttonPos;
        cursol.transform.localPosition = Buttons[currentButtonIndex].transform.localPosition;
        SetIconBetween();
    }

    protected override void SetNextSelect(int next)
    {
        if (next == 0) return;
        currentButtonIndex += next;
        buttonType = next < 0 ? ButtonType.Up : ButtonType.Down;
        cursol.transform.localPosition = Buttons[currentButtonIndex].transform.localPosition;
        SetIconBetween();
        audioSE?.PlayOneShot(audioSE.clip);
    }

    void SetIconBetween()
    {
        RectTransform button = Buttons[currentButtonIndex].GetComponent<RectTransform>();
        Vector3 icon = Vector3.right * (button.rect.width/2) + Vector3.right * IconAdjustmentValue;
        RightIcon.localPosition = icon;
        LeftIcon.localPosition = -icon;
    }
}
