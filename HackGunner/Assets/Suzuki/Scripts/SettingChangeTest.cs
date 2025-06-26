using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingChangeTest : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] string targetActionName;

    InputAction targetAction;

    private void Awake()
    {
        if (inputActions == null) return;

        targetAction = inputActions.FindAction(targetActionName);
        if(targetAction == null) return;

        targetAction.ApplyBindingOverride(new InputBinding
        {
            overrideProcessors = "scale(factor=10)"
        });
    }

    private void OnDestroy()
    {
        targetAction.Dispose();
    }
}
