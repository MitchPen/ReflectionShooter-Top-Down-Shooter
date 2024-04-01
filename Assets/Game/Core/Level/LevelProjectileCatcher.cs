using Game.Core.Common.Weapon.Projectiles;
using UnityEngine;

namespace Game.Core.Level
{
    public class LevelProjectileCatcher : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.TryGetComponent<BaseProjectile>(out BaseProjectile projectile))
                projectile.DisableBullet();
        }
    }
}
