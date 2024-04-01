using System;
using System.Collections.Generic;
using Game.Core.Common.Weapon.Projectiles;
using Game.Core.Common.Weapon.WeaponTypes;
using UnityEngine;

namespace Game.Core.Common.Weapon
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObjects/Configs/Weapon/WeaponConfig", order = 1)]
    public class WeaponConfigs : ScriptableObject
    {
        public List<WeaponSetup> weapons = new List<WeaponSetup>();
    }

    [Serializable]
    public class WeaponSetup
    {
        public BaseWeapon Weapon;
        public WeaponType Type;
        public WeaponData Data;
        public WeaponAmmoType AmmoType;
        public BaseProjectile Projectile;
    }
}
