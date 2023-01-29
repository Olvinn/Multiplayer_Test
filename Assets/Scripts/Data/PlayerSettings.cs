using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Game/Settings/PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField] public float moveSpeed { get; private set; }
    }
}
