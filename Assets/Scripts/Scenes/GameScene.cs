using System.Collections.Generic;
using System.Text;
using Cameras;
using Game;
using Inputs;
using Player;
using TMPro;
using UnityEngine;

namespace Scenes
{
    public class GameScene : MonoBehaviour
    {
        [SerializeField] private CameraController camera;
        [SerializeField] private TextMeshProUGUI scoreLabel;

        private List<PlayerController> _players;

        private StringBuilder _builder;
        
        private void Awake()
        {
            GameController.Instance.onCreatePlayer = CreatePlayer;

            _builder = new StringBuilder();
            _players = new List<PlayerController>();
        }

        private void Start()
        {
            camera.SetSettings(GameContext.Instance.CameraSettings);
        }

        private void Update()
        {
            _builder.Clear();
            foreach (var p in _players)
            {
                _builder.AppendLine($"{p.playerName}: {p.points}");
            }

            scoreLabel.text = _builder.ToString();
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
            
            _players.Add(player);
        }
    }
}
