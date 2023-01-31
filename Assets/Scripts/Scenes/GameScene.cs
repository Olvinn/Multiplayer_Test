using System.Collections.Generic;
using Cameras;
using Game;
using Inputs;
using Mirror;
using Player;
using UnityEngine;

namespace Scenes
{
    public class GameScene : MonoBehaviour
    {
        [SerializeField] private CameraController camera;

        private void Awake()
        {
            GameController.Instance.onCreatePlayer = CreatePlayer;
        }

        private void Start()
        {
            camera.SetSettings(GameContext.Instance.CameraSettings);
        }

        public void CreatePlayer(PlayerController player)
        {
            player.SetSettings(GameContext.Instance.PlayerSettings);
            
            if (player.isLocalPlayer)
            {
                player.camera = camera.camera.transform;
                camera.SetTarget(player.transform);
                
                InputController.Instance.attack = player.Dash;
                InputController.Instance.movement = player.Movement;
                InputController.Instance.rotation = camera.Rotate;
            }
        }
    }
}
