using Data;
using UnityEngine;

namespace Game
{
    public class GameContext : MonoBehaviour
    {
        public static GameContext Instance { get; private set; }

        [field: SerializeField] public PlayerSettings PlayerSettings { get; private set; }
        [field: SerializeField] public CameraSettings CameraSettings { get; private set; }
        
        public string name;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
    }
}
