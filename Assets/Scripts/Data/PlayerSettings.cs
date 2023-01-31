using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Game/Settings/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField] public float moveSpeed { get; private set; }
        [field: SerializeField] public float dashDistance { get; private set; }
        [field: SerializeField] public float dashSpeed { get; private set; }
        [field: SerializeField] public float dashDelay { get; private set; }
        [field: SerializeField] public float invulnarabilityDelay { get; private set; }
    }
}
