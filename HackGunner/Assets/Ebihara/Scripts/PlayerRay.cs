using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerRay : MonoBehaviour
{
    const float ChangeCastRadius = 0.4f;
    const float ChangeCheckRayOffset = 0.5f;
    [SerializeField] Change change;
    [SerializeField] float distance = 50.0f;//���o�\�ȋ���
    [SerializeField] LayerMask gazeHitMask;
    Transform transforms;
    GameObject game;
    PlayerMove playerMove;
    bool shoot;
    Vector3 rayHitPosition;

    public bool Shoot
    {
        get { return shoot; }
    }
    public Vector3 RayHitPosition
    {
        get { return rayHitPosition; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        shoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        //rayの始まり
        var rayStartPosition = this.transform.position;
        //rayの方向
        var rayDirection = this.transform.forward.normalized;
        Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);
        playerMove.Gun.transform.forward = rayDirection;

        Ray playerGaze = new Ray(rayStartPosition, rayDirection);
        if (Physics.Raycast(playerGaze, out RaycastHit hit, distance, gazeHitMask))
        {
            rayHitPosition = hit.transform.position;
        }
        else
        {
            rayHitPosition = transform.position + rayDirection * distance;
        }

        TargetManeger.PlayerStatus.CharacterAnimator.SetInteger("WeaponCategory", (int)playerMove.Gun.WeaponType);
        TargetManeger.PlayerStatus.CharacterAnimator.SetBool("AmmoKeep", playerMove.Gun.RemainBullets > 0);
    }

    public GameObject GetObj() { return game; }

    // ==================================================
    // 担当：鈴木十音
    // 概要：乗り移り機能・武器の射撃機能・射撃フラグの設定
    // ==================================================

    /// <summary>
    /// ボタンを押されたときの乗り移り処理
    /// </summary>
    /// <param name="context">該当キーやボタンの押されている等の情報</param>
    public void Change(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //rayの始まり
            var rayStartPosition = this.transform.position;

            //rayの方向
            var rayDirection = this.transform.forward.normalized;

            //Hitしたオブジェクト格納
            RaycastHit raycastHit;
            RaycastHit[] hits = Physics.SphereCastAll(rayStartPosition, ChangeCastRadius, rayDirection, distance);

            Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);

            foreach (RaycastHit hit in hits)
            {
                // 操作中のキャラクターでなくて視点の通る、HPが0のキャラクター
                if (TargetManeger.getPlayerObj() != hit.transform.gameObject && !Physics.Raycast(rayStartPosition, hit.transform.position + Vector3.up * ChangeCheckRayOffset - rayStartPosition, Vector3.Distance(hit.transform.position, rayStartPosition), LayerMask.GetMask("Stage", "Destructive"))
                    && hit.transform.TryGetComponent<CharacterStatus>(out CharacterStatus status) && status.CanPossess)
                {
                    character = hit.collider.gameObject;
                    change.ChangeEnemy(character);// 乗り移り処理

                    TargetManeger.PlayerStatus.CharacterAnimator.SetBool("Change", true);// 乗り移り前に操作していたキャラクターの頭を投げるアニメーションを再生
                    break;
                }
            }
        }
    }

    /// <summary>
    /// ボタンを押されたときの射撃処理
    /// </summary>
    /// <param name="context">該当キーやボタンの押されている等の情報</param>
    public void OnFire(InputAction.CallbackContext context)
    {
        if (PauseManager.IsPaused || change.Changing == true) return;// ポーズ中や乗り移り中は行わない
        if (context.phase == InputActionPhase.Performed)
        {
            // PlayerMoveに飛ばして弾を出す
            playerMove.Gun.Shoot(transform.position, playerMove.Gun.transform.forward, "Player", false);
            if (!shoot)
            {
                StartCoroutine(SetShootTrueForSeconds(0.2f));// 他のキャラクターがプレイヤーの射撃を検知するためのフラグを設定
                // Animatorに渡す
                TargetManeger.PlayerStatus.CharacterAnimator.SetBool("Fire", true);// 射撃アニメーションの再生
            }
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            TargetManeger.PlayerStatus.CharacterAnimator.SetBool("Fire", false);// 射撃アニメーションの中断
        }
    }

    /// <summary>
    /// 一定時間プレイヤーが射撃していたというフラグをオンにする（他のキャラクターが受け取れるように）
    /// </summary>
    /// <param name="second">オンにしておく時間</param>
    /// <returns>処理が完了するまでのコルーチン</returns>
    IEnumerator SetShootTrueForSeconds(float second)
    {
        shoot = true;
        yield return new WaitForSeconds(second);
        shoot = false;
    }
    // ===== ここまで =====
}
