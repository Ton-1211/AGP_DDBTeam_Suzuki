using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Change : MonoBehaviour
{
    //GameObject changeObj;

    PlayerMove playerMove;
    CharacterStatus characterStatus;


    [SerializeField] GameObject camera;
    PlayerRay playerRay;

　　[SerializeField] MainGameGunsUI gunsUI;
    [SerializeField] EnemyManager enemyManager;

    [SerializeField] GameObject playerHead;
    bool changing;
    bool changed;

    public bool Changed
    {
        get { return changed; }
    }

    public bool Changing
    {
        get { return changing; }
    }

    public float CharacterStatusHp
    {
        get { return characterStatus.Hp; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRay = camera.GetComponent<PlayerRay>();
        playerMove = this.GetComponent<PlayerMove>();
        characterStatus = transform.root.gameObject.GetComponent<CharacterStatus>();
        changing = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.transform.localEulerAngles.ToString());
    }

    public void ChangeEnemy(GameObject changeObj)
    {
        //仮
        //changeObj = playerRay.GetObj();
        characterStatus = changeObj.GetComponent<CharacterStatus>();

        //Debug.Log("乗り移る敵:" + changeObj.name);
        Debug.Log("Change");

        // 頭を飛ばしてからカメラ変更
        if (!changing)
        {
            StartCoroutine(DelayInstantiateHeadAndShoot(0.3f, changeObj));
        }
    }

    public void ChangeCameraTarget(GameObject changeObj)
    {
        //親をEnemyに
        //transform.parent.gameObject.tag = "Enemy";

        //親の付け替え
        this.gameObject.transform.parent = changeObj.transform;
        playerMove.SetplayerParent(this.transform.parent.gameObject);

        //親をPlayerに
        this.transform.parent.gameObject.tag = "Player";

        //銃の変更
        playerMove.SetGunObject();

        //Playerの位置調整
        this.transform.position = changeObj.transform.position;
        Vector3 correction = new Vector3(0f, 1.41f, 0.47f);
        this.transform.localPosition = correction;

        Vector3 angles = this.transform.localEulerAngles;
        angles.y = 0f;
        this.transform.localEulerAngles = angles;
        Debug.Log(this.transform.localEulerAngles.ToString());

        enemyManager.ResetSearch(playerMove.transform.position);
        changeObj = null;

        //if (targetManeger != null)
        TargetManeger.SetTarget(this.transform.parent.gameObject);
        gunsUI.AnimatorSetHead();
        if (!changed)
        {
            StartCoroutine(SetChangedTrueForSeconds(0.2f));
        }
        changing = false;
    }

    IEnumerator SetChangedTrueForSeconds(float second)
    {
        changed = true;
        yield return new WaitForSeconds(second);
        changed = false;
    }

    IEnumerator DelayInstantiateHeadAndShoot(float delaySeconds,GameObject changeObj)
    {
        gunsUI.AnimatorChangeHead();
        changing = true;
        yield return new WaitForSecondsRealtime(delaySeconds);
        PlayerHeadMoveScript playerHeadMoveScript = Instantiate(playerHead, transform.position + new Vector3(0f, -10f, 0f), Quaternion.identity).GetComponent<PlayerHeadMoveScript>();// プレイヤーの頭の位置からの生成に変更予定
        yield return StartCoroutine(playerHeadMoveScript.MoveHead(transform.position, characterStatus.transform, new Vector3(0f, 1.3f, 0f), changeObj, this));
    }
}
