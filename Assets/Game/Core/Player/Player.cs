using System;
using Game.Core.Common;
using Game.Core.Common.Services;
using Game.Core.Common.Weapon;
using UnityEngine;

namespace Game.Core.Player
{ 
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private GameObject model;
        [SerializeField] private Transform _cameraPoint;
        public Transform CameraFollowPoint => _cameraPoint;
        private bool _enabled;
        public PlayerComponents Components;
        private event Action _shotEvent;

        public void Init(InputService inputService, PlayerSetup setup)
        {
            Components.MovementComponent.Init(_characterController, setup.MovementConfig);
            Components.rotator.Init(inputService,model.transform);
            Components.WeaponController.Init(setup.WeaponConfigs.weapons);
        }

        public void EnablePlayer()
        {
            _enabled = true;
            _shotEvent += Components.WeaponController.TryShoot;
            Components.MovementComponent.enable = true;
            Components.rotator.enable = true;
            Components.WeaponController.enable = true;
        }
        
        public void DisablePlayer()
        {
            _enabled = false;
            _shotEvent -= Components.WeaponController.TryShoot;
            Components.MovementComponent.enable = false;
            Components.rotator.enable = false;
            Components.WeaponController.enable = false;
        }

        private void Update()
        {
            if (!_enabled) return;
            
            if(Input.GetKeyDown(KeyCode.Mouse0))
                _shotEvent?.Invoke();
        }
    }

    [Serializable]
    public class PlayerComponents
    {
        [SerializeField] public PlayerMovementComponent MovementComponent;
        [SerializeField] public PlayerRotator rotator;
        [SerializeField] public PlayerWeaponController WeaponController;
        [SerializeField] public HealthComponent HealthComponent;
    }
}