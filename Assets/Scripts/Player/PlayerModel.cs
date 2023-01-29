using System;
using UnityEngine;

namespace Player
{
    public class PlayerModel
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
    }
}
