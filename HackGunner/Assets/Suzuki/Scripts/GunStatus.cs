using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunStatus : MonoBehaviour
{
    const float DiffusionRateMax = 1f;
    const float DiffusionZ = 0f;
    
    [SerializeField] WeaponData weaponData;

    [UnityEngine.Serialization.FormerlySerializedAs("firstShotIntarval")][SerializeField] float firstShotInterval = 2f;
    [UnityEngine.Serialization.FormerlySerializedAs("defaultShotIntarval")][SerializeField] float defaultShotInterval = 0.5f;
    int remainBullets;

    public float FirstInterval => firstShotInterval;
    public float DefaultInterval => defaultShotInterval;

    public int RemainBullets => remainBullets;

    public Sprite WeaponImage => weaponData.WeaponImage;

    public bool IsSubWeapon => weaponData.SubWeapon == null;
    public WeaponData.WeaponType WeaponType => weaponData.Type;

    bool CanShoot => remainBullets > 0;// 撃てる状態か（残弾数が0より多いか）
    void Start()
    {
        FillBullet();// 最初は弾丸数を最大にする
    }
    /// <summary>
    /// 銃から発射方向を向いた銃弾を生成する
    /// </summary>
    /// <param name="infiniteBullet">弾を消費させるかどうか</param>
    /// <returns>撃てたかどうかを返す</returns>
    public bool Shoot(Vector3 position, Vector3 forward, string tag, bool infiniteBullet)
    {
        if(!CanShoot && !infiniteBullet) return false;// 残弾数が残っていないときは射撃しない（無限に撃てる状態の時を除く）
        if(!infiniteBullet) remainBullets--;

        for (int i = 0; i < weaponData.BulletSettings.Count; i++)
        {
            GameObject bullet = Instantiate(weaponData.BulletPrefab, position, Quaternion.identity);// 弾丸を生成

            // 拡散を計算
            Vector3 diffusion = new Vector3(weaponData.BulletSettings[i].Diffusion.x * Random.Range(DiffusionRateMax - weaponData.BulletSettings[i].RandomNess, DiffusionRateMax),
                weaponData.BulletSettings[i].Diffusion.y * Random.Range(DiffusionRateMax - weaponData.BulletSettings[i].RandomNess, DiffusionRateMax), DiffusionZ);

            bullet.tag = tag == "Player" ? "PlayerBullet" : "EnemyBullet";// 発射された弾丸がどちらの陣営かを設定
            bullet.transform.forward = forward;// 正面方向を設定
            bullet.transform.Rotate(diffusion);// 拡散の適用
        }
        SR_SoundController.instance.PlaySEOnce(weaponData.ShotSound, transform);// 銃声を鳴らす
        // 弾切れ時のサブ武器への切り替え
        if (!CanShoot && weaponData.Role == WeaponData.WeaponRole.Main)
        {
            ChangeWeapon();// サブ武器へと切り替える
        }
        return true;
    }

    void FillBullet()// 弾丸の補充
    {
        remainBullets = weaponData.MaxBullet;
    }

    void ChangeWeapon()// 武器の切り替え
    {
        if (weaponData.SubWeapon != null)
        {
            Transform parent = this.transform.parent;
            Instantiate(weaponData.SubWeapon, parent);// 武器の生成
            PlayerMove playerMove = parent.GetComponentInChildren<PlayerMove>();

            this.transform.SetParent(null);// 親子付けを外す
            playerMove.SetGunObject();// 持っている銃の設定
            Destroy(this.gameObject);
        }
    }
}
