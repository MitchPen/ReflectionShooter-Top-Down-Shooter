using UnityEngine;

namespace Game.Core.Common.Weapon
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Configs/Weapon/New Weapon Data", order = 1)]
    public class WeaponData: ScriptableObject
    {
        public int ammo;
        public float damage;
        public float fireRate;
        public float reloadTime;
        public int bulletReflectionCount;
        public float bulletSpeed;
        public int bulletPerShot;
        public float angleBetweenBullet;
        public AudioClip shootSound;
    }
}
