using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_Botu : MonoBehaviour
{
    [SerializeField] PlayerInput Input;
    bool aButton, bButton;
    Vector2 lStick, rStick;

    [SerializeField]
    Camera playerCamera;

    float rotaSpeed = 100f;
    [SerializeField]
    float Speed  = 3.0f;

    float upLim = 315f;   //ãŠp“xãŒÀ
    float downLim = 45f;   //‰ºŠp“xãŒÀ

    bool Attack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (Input == null) return;
        // ƒfƒŠƒQ[ƒg“o˜^
        Input.onActionTriggered += OnAbutton;
        Input.onActionTriggered += OnBbutton;
        Input.onActionTriggered += OnLstick;
        Input.onActionTriggered += OnRstick;
    }

    private void OnDisable()
    {
        if (Input == null) return;
        // ƒfƒŠƒQ[ƒg“o˜^‰ğœ
        Input.onActionTriggered -= OnAbutton;
        Input.onActionTriggered -= OnBbutton;
        Input.onActionTriggered -= OnLstick;
        Input.onActionTriggered -= OnRstick;
    }
    private void OnAbutton(InputAction.CallbackContext context)
    {
        if (context.action.name != "Abutton") return;
        if (!context.performed)
        {
            aButton = false;
            return;  
        }
        Debug.Log("Press");
        var isButton = context.ReadValueAsButton();
        // “ü—Í‚ğ•Û
        aButton = isButton;
    }
    private void OnBbutton(InputAction.CallbackContext context)
    {
        if (context.action.name != "Bbutton") return;
        // Action‚Ì“ü—Í’l‚ğæ“¾
        var isButton = context.ReadValueAsButton();
        // “ü—Í‚ğ•Û
        bButton = isButton;
    }
    private void OnLstick(InputAction.CallbackContext context)
    {
        if (context.action.name != "Lstick") return;
        // Action‚Ì“ü—Í’l‚ğæ“¾
        var isStick = context.ReadValue<Vector2>();
        // “ü—Í‚ğ•Û
        lStick = isStick;
    }
    private void OnRstick(InputAction.CallbackContext context)
    {
        if (context.action.name != "Rstick") return;
        // Action‚Ì“ü—Í’l‚ğæ“¾
        var isStick = context.ReadValue<Vector2>();
        // “ü—Í‚ğ•Û
        rStick = isStick;
    }

    // Update is called once per frame
    private void Update()
    {
        isAttack();
        isMovePlayer();
        //isMoveCamera();
    }
    private void FixedUpdate()
    {
        Debug.Log("Time.timeScale = 999");
    }
    void isMovePlayer()
    {
        Vector3 velocity = new Vector3(lStick.x, 0, lStick.y).normalized * Speed * Time.unscaledDeltaTime;
        this.transform.Translate(velocity);

        //Vector3 rotate = new Vector3(rStick.x, rStick.y, 0) * rotaSpeed * Time.deltaTime;
       
        //transform.Rotate(-rotate.y, rotate.x, 0);
    }
    void isMoveCamera()
    {
        Vector3 rotation = new Vector3(rStick.x, rStick.y, 0) * rotaSpeed * Time.deltaTime;

        if (rotation.x > downLim)
        {
            if (rotation.x > 180f)
            {
                if (upLim > rotation.x)
                {
                    rotation.x = upLim;
                }
            }
            else
            {
                rotation.x = downLim;
            }
        }
        playerCamera.transform.RotateAround(transform.position, Vector3.up, -rStick.x);
        playerCamera.transform.RotateAround(transform.position, Vector3.right, rStick.y);

    }
    void isAttack()
    {
        if (bButton)
        {
            Attack = true;
            Time.timeScale = 0;
        }
        else
        {
            Attack = false;
            Time.timeScale = 1;
            Speed = 5f;
        }
    }
}
