using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Game/Settings/CameraSettings")]
    public class CameraSettings : ScriptableObject
    { 
        [field: SerializeField] public float lerpSpeed { get; private set; } 
        [field: SerializeField] public float horizontalSpeed { get; private set; } 
        [field: SerializeField] public float verticalSpeed { get; private set; }
    }
}
