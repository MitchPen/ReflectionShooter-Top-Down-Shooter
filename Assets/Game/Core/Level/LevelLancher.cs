using Cinemachine;
using Game.Core.Common.Services;
using Game.Core.Player;
using UnityEngine;
using Zenject;

namespace Game.Core.Level
{
    public class LevelLauncher : MonoBehaviour
    {
        [Header("Camera")]
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [Inject] private InputService _input;
        [Inject] private SoundService _soundService;
        [SerializeField] private Player.Player _player;
        [SerializeField] private Level _level;
        [SerializeField] private PlayerSetup _playerSetup;
        [Inject] private DiContainer _diContainer;

        private void Start()
        {
            _input.SetCamera(_camera);
            _soundService.SetupEffectPlayerCount(10);
            _player = _diContainer.InstantiatePrefab(_player, _level.playerStartPoint.position, Quaternion.identity, _level.transform).GetComponent<Player.Player>();
            _player.Init(_input,_playerSetup);
            _virtualCamera.Follow = _player.CameraFollowPoint;
            //_virtualCamera.LookAt = _player.CameraFollowPoint;

            _input.EnableKeyHandle();
            _player.EnablePlayer();
        }
    }
}
