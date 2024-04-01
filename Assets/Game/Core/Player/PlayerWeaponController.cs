using System;
using System.Collections.Generic;
using Game.Core.Common;
using Game.Core.Common.Services;
using Game.Core.Common.Weapon;
using Game.Core.Common.Weapon.WeaponTypes;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Game.Core.Player
{
    public class PlayerWeaponController : MonoBehaviour
    {
        [Inject] private ObjectPool _pool;
        [Inject] private DiContainer _diContainer;
        public event Action<int> OnWeaponChanged;
        public List<BaseWeapon> _weapons { get; private set; }
        public bool enable;
        [SerializeField] private Transform _weaponPosition;
        [CanBeNull] private BaseWeapon _currentWeapon;
        private int _currentWeaponIndex = default;

        public void Init(List<WeaponSetup> weaponSetups)
        {
            _weapons = new List<BaseWeapon>();
            SetupWeapons(weaponSetups);
        }

        private void SetupWeapons(List<WeaponSetup> weaponSetups)
        {
            if (weaponSetups == null) return;
            weaponSetups.ForEach(ws =>
            {
                var weapon = _diContainer.InstantiatePrefab(ws.Weapon, _weaponPosition).GetComponent<BaseWeapon>()
                    .With(w => w.gameObject.SetActive(false))
                    .With(w => w.Setup(ws.Data, ws.Type, _pool));

                if(ws.AmmoType== WeaponAmmoType.PROJECTILE)
                    _pool.CreatePool(ws.Type.ToString(),ws.Projectile.gameObject,ws.Data.ammo*2);
                _weapons.Add(weapon);
            });
            SwitchWeapon();
        }

        public void TryShoot()
        {
            if (!enable || _currentWeapon == null) return;

            if (!_currentWeapon.IsEmpty())
            {
                if(_currentWeapon.AbleToShoot)
                    _currentWeapon.Shoot();
            }
            else
            {
                if(!_currentWeapon.IsReloading) 
                    _currentWeapon.Reload();
                SwitchWeapon(WeaponSwitchType.NOT_EMPTY);
            }
        }

        public void SwitchWeapon(WeaponSwitchType direction = WeaponSwitchType.NONE)
        {
            if (_weapons == null) return;

            switch (direction)
            {
                case WeaponSwitchType.NONE:
                {
                    if (_currentWeapon == null)
                        _currentWeaponIndex = 0;
                    break;
                }
                case WeaponSwitchType.NEXT:
                {
                    if (_currentWeaponIndex + 1 == _weapons.Count)
                        _currentWeaponIndex = 0;
                    else _currentWeaponIndex++;
                    break;
                }
                case WeaponSwitchType.PREV:
                {
                    if (_currentWeaponIndex - 1 < 0)
                        _currentWeaponIndex = _weapons.Count;
                    else _currentWeaponIndex--;
                    break;
                }
                case WeaponSwitchType.NOT_EMPTY:
                {
                    if (_weapons.Count == 1) return;
                    int notEmptyIndex = -1;
                    for (int i = 0; i < _weapons.Count; i++)
                    {
                        if (!_weapons[i].IsEmpty())
                        {
                            notEmptyIndex = i;
                            break;
                        }
                    }

                    if (notEmptyIndex <= 0)
                        _currentWeaponIndex = notEmptyIndex;
                    else return;
                    break;
                }
            }
            
            if(_currentWeapon!=null)
                _currentWeapon.gameObject.SetActive(false);
            
            _currentWeapon = _weapons[_currentWeaponIndex];
            _currentWeapon.gameObject.SetActive(true);
            OnWeaponChanged?.Invoke(_currentWeaponIndex);
        }
    }

    public enum WeaponSwitchType
    {
        NONE,
        NEXT,
        PREV,
        NOT_EMPTY
    }
}