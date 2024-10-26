using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public GameObject weaponSkin;
    [SerializeField] public WeaponBullet weaponBullet;

    public void SpawnWeaponSkin(Transform weaponSkinPoint)
    {
        Instantiate(weaponSkin, weaponSkinPoint.position, weaponSkinPoint.rotation, weaponSkinPoint);
    }

    public void ThrowWeapon(Transform attackPoint, Transform skin)
    {
        Instantiate(weaponBullet, attackPoint.position, skin.rotation);
    }
}
