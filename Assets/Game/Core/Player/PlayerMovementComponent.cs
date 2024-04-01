using UnityEngine;

namespace Game.Core.Player
{
    public class PlayerMovementComponent : MonoBehaviour
    {
        private PlayerMovementConfig _movementConfig;
        private GameObject _input;
        private CharacterController _playerChar;
        private Vector3 _nextPos;
        public bool enable;
        private Vector2 TEST_Direction;

        public void Init(CharacterController playerBody, PlayerMovementConfig cfg)
        {
            _nextPos = Vector3.zero;
            TEST_Direction = Vector2.zero;
            _playerChar = playerBody;
            _movementConfig = cfg;
        }

        private void Update()
        {
            if(!enable) return;
            TEST_Direction.y = Input.GetAxisRaw("Vertical");
            TEST_Direction.x = Input.GetAxisRaw("Horizontal");
            if(TEST_Direction!=Vector2.zero)
                Move(TEST_Direction);
        }

        private void Move(Vector2 direction)
        {
            _nextPos.x = direction.x;
            _nextPos.z = direction.y;
            var motion = _nextPos.normalized * (_movementConfig.Speed * Time.deltaTime);
            _playerChar.Move(motion);
        }
    }
}
