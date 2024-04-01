using Game.Core.Common.Services;
using Game.Core.Common.Weapon.Projectiles;
using UnityEngine;

namespace Game.Core.Common.Weapon.WeaponTypes
{
    public class ProjectileWeapon : BaseWeapon
    {
        [SerializeField] private Transform _firePoint;
        
        public override void Shoot()
        {
            base.Shoot();
            switch (Type)
            {
                case WeaponType.PISTOL: SingleShot(_firePoint.forward.normalized); break;
                case WeaponType.SHOTGUN: MultipleShoot(); break;
            }
            OnShot?.Invoke();
        }

        private void SingleShot(Vector3 direction)
        {
            var newBullet = _pool.GetFromPoolWithType<BaseProjectile>(Type.ToString());

            newBullet
                .SetDamage(Data.damage)
                .SetSpeed(Data.bulletSpeed)
                .SetMaxReflectCount(Data.bulletReflectionCount)
                .SetDirection(direction)
                .With(b=>b.transform.position = _firePoint.position)
                .With(b=> b.OnDisable=() =>
                {
                    _soundService.PlaySound(b.DisableSound);
                    _pool.ReturnToPool(Type.ToString(),b.gameObject);
                })
                .With(b=>b.OnReflect= () =>
                {
                    _soundService.PlaySound(b.ReflectSound);
                })
                .StartMotion();
            //CurremtAmmo--;
        }

        private void MultipleShoot()
        {
            var dispersion = Data.bulletPerShot * Data.angleBetweenBullet;
            var dispersionHalf = dispersion / 2;
        }
    }
}
