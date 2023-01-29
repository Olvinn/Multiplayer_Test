using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Game/Settings/CameraSettings")]
    public class CameraSettings : ScriptableObject
    { 
        [field: SerializeField] public float LerpSpeed { get; private set; } 
        [field: SerializeField] public float HorizontalSpeed { get; private set; } 
        [field: SerializeField] public float VerticalSpeed { get; private set; }
        [field: SerializeField] public float FieldOfView { get; private set; }
    }
}
