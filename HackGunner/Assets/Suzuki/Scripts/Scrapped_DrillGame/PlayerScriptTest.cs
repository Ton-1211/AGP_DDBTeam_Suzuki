using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScriptTest : MonoBehaviour
{
    [SerializeField] MapScript mapScript;
    [SerializeField] float speed;
    Vector2 moveVector;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent<Rigidbody>(out rb))// RigidBody���A�^�b�`����Ă��Ȃ������Ƃ��̉��}���u
        {
            gameObject.AddComponent<Rigidbody>();
            Debug.LogError("Play����߂����Rigidbody�R���|�[�l���g���A�^�b�`���Ă��������B");
        }
    }


    void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started || context.phase == InputActionPhase.Performed)
        {
            moveVector = context.ReadValue<Vector2>();
            moveVector.Normalize();// �΂߈ړ��������Ȃ�Ȃ��悤��
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            moveVector = Vector2.zero;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Ray downRay = new Ray(transform.position, -transform.up);
            RaycastHit hit;
            if(Physics.Raycast(downRay, out hit))
            {
                mapScript.BreakGrid(hit.collider.gameObject);
            }
        }
    }

    private void Move()
    {
        rb.AddForce(new Vector3(moveVector.x , 0f, moveVector.y) * speed, ForceMode.Impulse);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveVector.magnitude * speed);// �ړ����x�̐���
    }
    // �e�X�g
}
