using System;
using UnityEngine;

namespace Game.Core.Common
{
    [RequireComponent(typeof(Collider))]
    public class HealthComponent : MonoBehaviour
    {
        public event Action<float> OnHpUpdated;
        public event Action OnZeroHealthPoint;
        private float _maxHp;
        public float CurrentHP { get; private set; }

        public void Init(float hp)
        {
            CurrentHP = hp;
            _maxHp = CurrentHP;
            OnHpUpdated?.Invoke(CurrentHP);
        }

        public void UpdateHP(float value)
        {
            CurrentHP = Math.Clamp(CurrentHP + value, 0, _maxHp);
            if (CurrentHP == 0)
                OnZeroHealthPoint?.Invoke();
            OnHpUpdated?.Invoke(CurrentHP);
        }
    }
}