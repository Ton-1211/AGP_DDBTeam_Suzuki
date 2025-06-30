using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunStatus : MonoBehaviour
{
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

    void Start()
    {
        FillBullet();
    }
    /// <summary>
    /// 銃から発射方向を向いた銃弾を生成する
    /// </summary>
    /// <param name="infiniteBullet">弾を消費させるかどうか</param>
    /// <returns>撃てたかどうかを返す</returns>
    public bool Shoot(Vector3 position, Vector3 forward, string tag, bool infiniteBullet)
    {
        if(remainBullets <= 0 && !infiniteBullet) return false;// 残弾数が残っていないときは射撃しない（無限に撃てる状態の時を除く）
        if(!infiniteBullet) remainBullets--;

        for (int i = 0; i < weaponData.BulletSettings.Count; i++)
        {
            GameObject bullet = Instantiate(weaponData.BulletPrefab, position, Quaternion.identity);

            Vector3 diffusion = new Vector3(weaponData.BulletSettings[i].Diffusion.x * Random.Range(1 - weaponData.BulletSettings[i].RandomNess, 1),
                weaponData.BulletSettings[i].Diffusion.y * Random.Range(1 - weaponData.BulletSettings[i].RandomNess, 1), 0f);

            bullet.tag = tag == "Player" ? "PlayerBullet" : "EnemyBullet";
            bullet.transform.forward = forward;
            bullet.transform.Rotate(diffusion);
        }
        SR_SoundController.instance.PlaySEOnce(weaponData.ShotSound, transform);// 銃声を鳴らす
        if (remainBullets == 0 && weaponData.Role == WeaponData.WeaponRole.Main)
        {
            ChangeWeapon();
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
