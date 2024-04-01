using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Common.Services;
using UnityEngine;
using Zenject;

namespace Game.Core.Common.Weapon
{
    public abstract  class BaseWeapon: MonoBehaviour
    {
        [Inject] protected SoundService _soundService;
        public Action OnShot;
        public Action<float> OnReloadStart;
        public WeaponType Type { get; protected set; }
        public int CurremtAmmo { get; protected set; }
        public bool IsReloading { get; private set; }
        public bool AbleToShoot { get; protected set; }
        protected WeaponData Data;
        protected ObjectPool _pool;
        [SerializeField] private AudioSource _audio;
        private int _maxAmmo;
        private int _reloadTime;
        private int _shootingDelay;
        private CancellationTokenSource _reloadCts;
        private CancellationTokenSource _shootDelayCts;

        public void Setup(WeaponData data, WeaponType type, ObjectPool pool)
        {
            Data = data;
            Type = type;
            _maxAmmo = data.ammo;
            CurremtAmmo = _maxAmmo;
            _pool = pool;
            _reloadCts = new CancellationTokenSource();
            _shootDelayCts = new CancellationTokenSource();
            
            _reloadTime = (int)(Data.reloadTime * 1000);
            _shootingDelay = (int)(Data.fireRate * 1000);
            AbleToShoot = true;
        }
        public bool IsEmpty() => CurremtAmmo <= 0;

        public virtual void Shoot()
        {
            ApplyShootingDelay();
            _soundService.PlaySound(Data.shootSound);
        }

        public async void Reload()
        {
            IsReloading = true;
            OnReloadStart?.Invoke(Data.reloadTime);
            await UniTask.Delay(_reloadTime, cancellationToken:_reloadCts.Token).SuppressCancellationThrow();
            CurremtAmmo = _maxAmmo;
            IsReloading = false;
        }

        private async void ApplyShootingDelay()
        {
            AbleToShoot = false;
            await UniTask.Delay(_shootingDelay, cancellationToken:_shootDelayCts.Token).SuppressCancellationThrow();
            AbleToShoot = true;
        }

        private void OnDestroy()
        {
            CancelToken(_reloadCts);
            CancelToken(_shootDelayCts);
        }

        private void CancelToken(CancellationTokenSource token)
        {
            if (token == null) return;

            if (!token.IsCancellationRequested)
            {
                token.Dispose();
                token.Dispose();
            }
        }
    }
}
