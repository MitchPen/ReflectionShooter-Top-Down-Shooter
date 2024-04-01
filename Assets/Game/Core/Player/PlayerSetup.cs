using Game.Core.Common.Weapon;
using UnityEngine;

namespace Game.Core.Player
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "ScriptableObjects/Configs/Player/Player Setup", order = 1)]
    public class PlayerSetup : ScriptableObject
    {
        public PlayerMovementConfig MovementConfig;
        public WeaponConfigs WeaponConfigs;
    }
}
