using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeSensitivity : MonoBehaviour
{
    [SerializeField] InputActionAsset actionAsset;
    [SerializeField] string actionName;

    InputAction target;

    // Start is called before the first frame update
    void Start()
    {
        if (actionAsset == null)
            return;

        target = actionAsset.FindAction(actionName);
        if (target == null) return;

        target.ApplyBindingOverride(new InputBinding
        {
            overrideProcessors = "scale(factor=10)"
        });

        actionAsset.Enable();
    }

    private void Awake()
    {
        //if (actionAsset == null)
        //    return;

        //target = actionAsset.FindAction(actionName);
        //if(target==null) return;

        //target.ApplyBindingOverride(new InputBinding
        //{
        //    overrideProcessors="scale(factor=10)"
        //});
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        target.Dispose();
    }
}
