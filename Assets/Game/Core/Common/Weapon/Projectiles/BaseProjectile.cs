using System;
using UnityEngine;

namespace Game.Core.Common.Weapon.Projectiles
{
    public class BaseProjectile : MonoBehaviour
    {
        public Action OnDisable;
        public Action OnReflect; 
        public AudioClip ReflectSound;
        public AudioClip DisableSound;
        [SerializeField] private Rigidbody _body;
        private float _damage =0;
        private float _speed = 0;
        private int _collisionCountLeft = 0;
        private Vector3 _direction = Vector3.zero;

        public BaseProjectile SetSpeed(float speed)
        {
            _speed = speed;
            return this;
        }

        public BaseProjectile SetDamage(float damage)
        {
            _damage = damage;
            return this;
        }
        public BaseProjectile SetDirection(Vector3 direction)
        {
            _direction = direction;
            return this;
        }

        public BaseProjectile SetMaxReflectCount(int count)
        {
            _collisionCountLeft = count;
            return this;
        }

        public void StartMotion()
        {
            _body.maxLinearVelocity = _speed;
            gameObject.SetActive(true);
            _body.velocity = _direction * _speed;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<HealthComponent>(out HealthComponent hp))
            {
                hp.UpdateHP(-_damage);
                DisableBullet();
            }
            else
            {
                _collisionCountLeft--;
                if (_collisionCountLeft > 0)
                {
                    _body.velocity = Vector3.zero;
                    _direction = Vector3.Reflect(_direction, other.contacts[0].normal);
                    _body.velocity = _direction * _speed;
                    OnReflect?.Invoke();
                }
                else
                    DisableBullet();
            }
        }

        public void DisableBullet()
        {
            _body.velocity = Vector3.zero;
            gameObject.SetActive(false);
            OnDisable?.Invoke();
            OnDisable = null;
            OnReflect = null;
        }
    }
}
