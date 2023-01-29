using Data;
using UnityEngine;

namespace Game
{
    public class GameContext : MonoBehaviour
    {
        public static GameContext Instance { get; private set; }

        [field: SerializeField] public PlayerSettings playerSettings { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
    }
}
