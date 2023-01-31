using System;
using Game;
using Mirror;
using UnityEngine;

namespace Player
{
    public class PlayerModel : NetworkBehaviour
    {
        public Action<Vector3> onPositionChanged;
        
        [SyncVar(hook = nameof(OnPositionChanged))]
        public Vector3 position;

        [SyncVar(hook = nameof(OnVelocityChanged))]
        public Vector3 velocity;

        private void Start()
        {
            GameController.Instance.RegisterPlayer(this);
            syncInterval = .05f;
        }

        [Command]
        public void SetPosition(Vector3 pos)
        {
            position = pos;
        }

        [Command]
        public void SetVelocity(Vector3 v)
        {
            velocity = v;
        }

        private void OnPositionChanged(Vector3 old, Vector3 newPos)
        {
            onPositionChanged?.Invoke(newPos);
            position = newPos;
        }

        private void OnVelocityChanged(Vector3 old, Vector3 newV)
        {
            velocity = newV;
        }
    }
}
