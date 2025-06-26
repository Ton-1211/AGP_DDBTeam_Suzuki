using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayTemp : MonoBehaviour
{
    [SerializeField] Camera tpsCam;
    [SerializeField] float distance = 50.0f;    //���o�\�ȋ���
    GameObject objParent;
    Move Move;
    Transform transforms;

    // Start is called before the first frame update
    void Start()
    {
        objParent = transform.parent.gameObject;
        Move= objParent.GetComponent<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        //�J�����̈ʒu����Ƃ΂�
        var rayStartPosition = tpsCam.transform.position;

        //�J�����������Ă�����ɂƂ΂�
        var rayDirection = tpsCam.transform.forward.normalized;

        Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //�J�����̈ʒu����Ƃ΂�
            var rayStartPosition = tpsCam.transform.position;

            //�J�����������Ă�����ɂƂ΂�
            var rayDirection = tpsCam.transform.forward.normalized;

            //Hit�����I�u�W�F�N�g�i�[�p
            RaycastHit raycastHit;

            Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);

            if (Physics.Raycast(rayStartPosition, rayDirection, out raycastHit, distance))
            {
                // Log��Hit�����I�u�W�F�N�g�����o��
                //Debug.Log(context.phase);
                Debug.Log("HitObject : " + raycastHit.collider.gameObject.name);

                if (raycastHit.collider.tag == "Enemy")
                {
                    Debug.Log("EnemyHit");
                    transforms = raycastHit.transform;
                }
            }
        }
    }

    public void ChangeEnemy(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && transforms != null)
        {
            Debug.Log("Change");

            //�e��Enemy��
            transform.parent.gameObject.tag = "Enemy";
            objParent.transform.rotation = Quaternion.identity;
            Move.enabled = false;

            //�e�̕t���ւ�
            this.gameObject.transform.parent = transforms;
            objParent = transform.parent.gameObject;
            Move = objParent.GetComponent<Move>();

            //�e��Player��
            transform.parent.gameObject.tag = "Player";
            Move.enabled = true;

            //_cam.Follow = raycastHit.transform;
            this.transform.position = transforms.position;
            this.transform.localPosition = new Vector3(0f, 1.5f, -3f);
            this.transform.localRotation = Quaternion.identity;

            transforms = null;
        }
    }
}
