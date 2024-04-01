using UnityEngine;

namespace Game.Core.Player
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "ScriptableObjects/Configs/Player/New Movement Config", order = 1)]
    public class PlayerMovementConfig : ScriptableObject
    {
        public float Speed;
        public float DashDistance;
        public float DashCooldown;
    }
}
