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
            Debug.LogWarning("このオブジェクトにConeCollider がアタッチされていません。");
        }
        player = null;
        inSearchArea = false;
        findPlayer = false;
    }

    /// <summary>
    /// Raycastを行う再起関数
    /// </summary>
    /// <param name="repeat">Raycastを行う回数</param>
    /// <param name="target">Targetの位置。繰り返すごとにY座標0.1f上昇</param>
    /// <param name="hit">RaycastHitの参照渡し</param>
    /// <returns>Hitしたかを返す。trueの場合、RaycastHitに情報を格納</returns>
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
            if (raycast)// プレイヤーが隠れていないとき（現在は照射した一点が通るかどうかで判定している）
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
        //if(other.TryGetComponent<PlayerMove>(out PlayerMove playerMove) && playerMove.enabled)// プレイヤーだったとき
        if (other.gameObject.tag == "Player")
        {
            inSearchArea = true;
            player = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if (other.TryGetComponent<PlayerMove>(out PlayerMove playerMove) && playerMove.enabled)// プレイヤーだったとき
        if (other.gameObject.tag == "Player")
        {
            inSearchArea = false;
        }
    }

    /// <summary>
    /// プレイヤーの発見情報をリセット
    /// </summary>
    public void OnPlayerChange()
    {
        inSearchArea = false;
        player = null;
    }
}
