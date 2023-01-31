using System;
using Game;
using Mirror;
using UnityEngine;

namespace Player
{
    public class PlayerModel : NetworkBehaviour
    {
        public Action<Vector3> onPositionChanged; 
        public Vector3 position
        {
            get { return _position; }
            set
            {
                _position = value;
                onPositionChanged?.Invoke(_position);
            }
        }

        private Vector3 _position;

        private void Start()
        {
            GameController.Instance.RegisterPlayer(this);
        }
    }
}
