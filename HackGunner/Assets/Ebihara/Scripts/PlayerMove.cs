using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cinemachine;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    Vector2 input, inputR;

    float moveZ;
    float moveX;

    [SerializeField] float moveSpeed = 6f; //スピード

    Vector3 velocity = Vector3.zero; //向き

    GameObject playerParent;
    GunStatus gun;

    [SerializeField] CinemachineInputProvider inputProvider;
    [SerializeField] InputActionReference look;
    [SerializeField] InputActionReference aim;
    [SerializeField] AudioClip walkSound;
    bool isAiming;
    bool isWalking;
    float walkSoundTimer = 0f;
    float walkSoundTimerMax = 0.5f;
    //bool isChangeMode;

    //CinemachineFramingTransposer transposer;
    //[SerializeField] float fpsDistance;
    //[SerializeField] float tpsDistance;

    //[SerializeField] CinemachineVirtualCamera lookCamera;

    [SerializeField] GameObject camera;
    PlayerRay playerRay;
    GameObject objParent;
    Change change;

    //親
    public GameObject PlayerParent
    {
        get { return playerParent; }
    }
    public GunStatus Gun
    {
        get { return gun; }
    }

    //public bool IsChangeGame
    //{
    //    get { return isChangeMode; }
    //}

    // Start is called before the first frame update
    void Start()
    {
        playerParent = transform.parent.gameObject;
        playerRay = camera.GetComponent<PlayerRay>();
        change = GetComponent<Change>();
        SetGunObject();
        isAiming = false;

        //isChangeMode= false;

        //transposer = lookCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!change.Changing && !PauseManager.IsPaused)
        {
            //カメラの方向に向く
            Vector3 direction = camera.transform.position - this.transform.position;

            Vector3 lookdirection = new Vector3(direction.x * -1.0f, 0.0f, direction.z * -1.0f);

            playerParent.transform.rotation = Quaternion.LookRotation(lookdirection);

            //前後
            moveZ = input.y;
            //左右
            moveX = input.x;

            float deltaTime = PauseManager.IsSlow ? Time.unscaledDeltaTime : Time.deltaTime;
            velocity = new Vector3(moveX, 0, moveZ).normalized * moveSpeed * deltaTime;
            playerParent.transform.Translate(velocity.x, velocity.y, velocity.z);
        }
        if(isWalking)
        {
            PlayWalkSound();
        }
    }

    public void SetplayerParent(GameObject gameObject)
    {
        playerParent = gameObject;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (PauseManager.IsPaused) return;
        input = context.ReadValue<Vector2>();

        if (context.phase == InputActionPhase.Started) isWalking = true;
        if (context.phase == InputActionPhase.Canceled) isWalking = false;
        //Debug.Log(input);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (PauseManager.IsPaused) return;
        if (Physics.Raycast(playerParent.transform.position, Vector3.down, 0.5f, LayerMask.GetMask("Stage")))
        {
            playerParent.GetComponent<Rigidbody>().AddForce(0f, 1.4f, 0f, ForceMode.Impulse);
        }
    }
    public void ChangeAim(InputAction.CallbackContext context)
    {
        //Vector3 angle = this.transform.parent.localEulerAngles;

        if (isAiming == false)
        {
            isAiming = true;
            //Debug.Log("before:" + aimCamera.transform.eulerAngles);     
            //aimCamera.transform.localEulerAngles = angle;
            //Debug.Log("after:" + aimCamera.transform.eulerAngles);
            inputProvider.XYAxis = aim;

            //transposer.m_CameraDistance = aimDistance;//徐々に近づけるようにする

            //aimCamera.Priority = 1;
            //lookCamera.Priority = 0;
        }
        else
        {
            isAiming = false;
            //Debug.Log("before:" + aimCamera.transform.eulerAngles);
            //lookCamera.transform.localEulerAngles = angle;
            //Debug.Log("after:" + aimCamera.transform.eulerAngles);
            inputProvider.XYAxis = look;
            //transposer.m_CameraDistance = lookDistance;
            //lookCamera.Priority = 1;
            //aimCamera.Priority = 0;
        }
    }

    //public void ChangeMode(InputAction.CallbackContext context)
    //{
    //    if (isChangeMode == false)
    //    {
    //        isChangeMode= true;
    //        transposer.m_CameraDistance = tpsDistance;
    //    }
    //    else
    //    {
    //        isChangeMode= false;
    //        transposer.m_CameraDistance = fpsDistance;
    //    }
    //}

    public void OnLook(InputAction.CallbackContext context)
    {
        if(PauseManager.IsPaused) return;
        inputR = context.ReadValue<Vector2>();
        //Debug.Log("Look");
    }

    public void ChangeEnemy(GameObject game)
    {
        objParent = game;

        if (objParent != null)
        {
            Debug.Log("�|�����G:" + objParent.name);

            Debug.Log("Change");
            Vector3 quaternion = objParent.transform.eulerAngles;
            Debug.Log(quaternion);

            //親のタグをEnemyに
            transform.parent.gameObject.tag = "Enemy";

            //親の付け替え
            this.gameObject.transform.parent = objParent.transform;
            playerParent = this.transform.parent.gameObject;
            playerParent.transform.eulerAngles = quaternion;

            //親のタグをPlayerに
            this.transform.parent.gameObject.tag = "Player";

            //Playerの位置調整
            this.transform.position = objParent.transform.position;
            Vector3 correction = new Vector3(0f, 1.5f, 0f);
            this.transform.position += correction;

            objParent = null;
        }
    }

    public void SetGunObject()
    {
        gun = playerParent.GetComponentInChildren<GunStatus>();
    }

    void PlayWalkSound()
    {
        walkSoundTimer -= Time.unscaledDeltaTime;
        if (walkSoundTimer <= 0f)
        {
            if (Physics.Raycast(playerParent.transform.position, Vector3.down, 0.5f, LayerMask.GetMask("Stage")))
            {
                SR_SoundController.instance.PlaySEOnce(walkSound, transform);
                walkSoundTimer = walkSoundTimerMax;
            }
        }
    }
}
