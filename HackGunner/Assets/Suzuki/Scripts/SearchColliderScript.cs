using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchColliderScript : MonoBehaviour
{
    ConeCollider coneCollider;
    Transform player;
    bool inSearchArea = false;
    bool findPlayer = false;
    [SerializeField] LayerMask layerMask;
    [SerializeField] int repeatCount = 5;
    public Transform FoundPlayer
    {
        get { return player != null ? player : null; }
    }
    public bool IsFindPlayer
    {
        get { return findPlayer; }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent<ConeCollider>(out coneCollider))
        {
            Debug.LogWarning("���̃I�u�W�F�N�g��ConeCollider ���A�^�b�`����Ă��܂���B");
        }
        player = null;
        inSearchArea = false;
        findPlayer = false;
    }

    /// <summary>
    /// Raycast���s���ċN�֐�
    /// </summary>
    /// <param name="repeat">Raycast���s����</param>
    /// <param name="target">Target�̈ʒu�B�J��Ԃ����Ƃ�Y���W0.1f�㏸</param>
    /// <param name="hit">RaycastHit�̎Q�Ɠn��</param>
    /// <returns>Hit��������Ԃ��Btrue�̏ꍇ�ARaycastHit�ɏ����i�[</returns>
    bool RaycastRepeat(int repeat,Vector3 target,ref RaycastHit hit)
    {
        Vector3 targetDirection = target + Vector3.up * 0.1f;
        Ray ray = new Ray(transform.position, targetDirection);
        bool raycast = Physics.Raycast(ray, out hit, coneCollider.Distance, layerMask);
        if(raycast)return true;
        else
        {
            if(repeat == 0)
                return false;
            return RaycastRepeat(repeat - 1, targetDirection,ref hit);
        }
    }

    void FixedUpdate()
    {
        if (inSearchArea && player != null)
        {
            /*
            Vector3 targetDirection = player.position - transform.position;
            Ray ray = new Ray(transform.position, targetDirection);
            bool raycast = Physics.Raycast(ray, out RaycastHit hit, coneCollider.Distance, layerMask);
            */
            RaycastHit hit = new();
            bool raycast = RaycastRepeat(repeatCount, player.position - transform.position,ref hit);
            if (raycast)// �v���C���[���B��Ă��Ȃ��Ƃ��i���݂͏Ǝ˂�����_���ʂ邩�ǂ����Ŕ��肵�Ă���j
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    findPlayer = true;
                    Debug.Log("FindPlayer : true");
                }
                else
                {
                    findPlayer = false;
                    Debug.Log("FindPlayer : false");
                }
            }
        }
        else
        {
            findPlayer = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        //if(other.TryGetComponent<PlayerMove>(out PlayerMove playerMove) && playerMove.enabled)// �v���C���[�������Ƃ�
        if (other.gameObject.tag == "Player")
        {
            inSearchArea = true;
            player = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if (other.TryGetComponent<PlayerMove>(out PlayerMove playerMove) && playerMove.enabled)// �v���C���[�������Ƃ�
        if (other.gameObject.tag == "Player")
        {
            inSearchArea = false;
        }
    }

    /// <summary>
    /// �v���C���[�̔����������Z�b�g
    /// </summary>
    public void OnPlayerChange()
    {
        inSearchArea = false;
        player = null;
    }
}
