using System;
using UnityEngine;

namespace Inputs
{
    public abstract class InputController : MonoBehaviour
    {
        public static InputController Instance { get; private set; }
        
        public Action<Vector2> movement;
        public Action<Vector2> rotation;
        public Action attack;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
    }
}
