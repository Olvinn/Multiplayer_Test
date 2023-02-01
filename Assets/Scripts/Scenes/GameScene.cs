using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cameras;
using Game;
using Inputs;
using Mirror;
using Player;
using TMPro;
using UnityEngine;

namespace Scenes
{
    public class GameScene : MonoBehaviour
    {
        [SerializeField] private CameraController camera;
        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private TextMeshProUGUI winner;
        [SerializeField] private TextMeshProUGUI ip;

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

            ip.text = NetworkManager.singleton.networkAddress;
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

        private void ShowWinner(string winner)
        {
            this.winner.gameObject.SetActive(true);
            this.winner.text = $"The winner is {winner}!";
            StartCoroutine(RestartTimer(5,$"The winner is {winner}!"));
        }

        public void CreatePlayer(PlayerController player)
        {
            player.SetSettings(GameContext.Instance.PlayerSettings);
            player.onShowWinner = ShowWinner;
            
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

        private void OnDestroy()
        {
            InputController.Instance.attack = null;
            InputController.Instance.movement = null;
            InputController.Instance.rotation = null;
        }

        IEnumerator RestartTimer(float timer, string text)
        {
            while (timer > 0)
            {
                this.winner.text = $"{text}!\n{timer :F1}";
                yield return null;
                timer -= Time.deltaTime;
            }
            this.winner.gameObject.SetActive(false);
        }
    }
}
