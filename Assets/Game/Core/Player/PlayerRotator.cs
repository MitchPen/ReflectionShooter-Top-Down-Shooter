using Game.Core.Common.Services;
using UnityEngine;

namespace Game.Core.Player
{
    public class PlayerRotator : MonoBehaviour
    {
        private Transform _rotationPoint;
        private Vector3 _direction;
        private InputService _input;
        private Vector3 _rotationPos=>_rotationPoint.position;
        public bool enable;
        public void Init(InputService inputService,Transform rotationPoint)
        {
            _input = inputService;
            _rotationPoint = rotationPoint;
        }

        private void Update()
        {
            if(!enable) return;
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            var (succes, position) = _input.GetMousePosition();
            if (succes)
            {
                position.y = _rotationPos.y;
                _direction = position - _rotationPos;
            }

            if (_direction!=Vector3.zero)
                _rotationPoint.forward = _direction;
        }
    }
}
