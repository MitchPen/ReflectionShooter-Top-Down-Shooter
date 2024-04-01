using System;
using UnityEngine;
using Zenject;

namespace Game.Core.Common.Services
{
    public class SoundService : MonoBehaviour
    {
        private const String EFFECT_KEY = "EFFECT_KEY";
        [Inject] private ObjectPool _pool;
        [SerializeField] private AudioSource _MusicPlayer;
        [SerializeField] private AudioSource _effectPlayer;

        public void SetupEffectPlayerCount(int count)
        {
            _pool.CreatePool(EFFECT_KEY, _effectPlayer.gameObject, count);
        }
        public void PlaySound(AudioClip sound, bool loop = false)
        {
            Debug.Log("call_SoundService");
            var player = _pool.GetFromPoolWithType<AudioSource>(EFFECT_KEY);
            player.gameObject.SetActive(true);
            player.clip = sound;
            player.loop = loop;
            player.Play();
            _pool.ReturnToPool(EFFECT_KEY,player.gameObject);
        }
    }
}
