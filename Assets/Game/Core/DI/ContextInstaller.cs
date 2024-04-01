using Game.Core.Common;
using Game.Core.Common.Services;
using UnityEngine;
using Zenject;

namespace Game.Core.DI
{
    public class ContextInstaller : MonoInstaller
    {
        [Header("Services")]
        [SerializeField] private ObjectPool _pool;
        [SerializeField] private SoundService _soundService;
        [SerializeField] private InputService _inputService;
        public override void InstallBindings()
        {
            Container.Bind<ObjectPool>().FromInstance(_pool).AsSingle().NonLazy();
            Container.Bind<SoundService>().FromInstance(_soundService).AsSingle().NonLazy();
            Container.Bind<InputService>().FromInstance(_inputService).AsSingle().NonLazy();
        }
    }
}