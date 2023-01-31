using System.Collections.Generic;
using Cameras;
using Game;
using Inputs;
using Player;
using UnityEngine;

namespace Scenes
{
    public class GameScene : MonoBehaviour
    {
        [SerializeField] private CameraController camera;
        [SerializeField] private List<PlayerController> freePlayers;
        [SerializeField] private PlayerController localPlayer;
        [SerializeField] private Transform[] spawnPositions;

        private Dictionary<PlayerModel, PlayerController> _busyPlayers;

        private void Awake()
        {
            GameController.Instance.onCreatePlayer = CreatePlayer;

            _busyPlayers = new Dictionary<PlayerModel, PlayerController>();
        }

        private void Start()
        {
            localPlayer.SetSettings(GameContext.Instance.PlayerSettings);
            foreach (var p in freePlayers)
            {
                p.SetSettings(GameContext.Instance.PlayerSettings);
            }
            camera.SetSettings(GameContext.Instance.CameraSettings);
            
            InputController.Instance.attack = localPlayer.Dash;
            InputController.Instance.movement = localPlayer.Movement;
            InputController.Instance.rotation = camera.Rotate;
        }

        public void CreatePlayer(PlayerModel model)
        {
            var spwn = spawnPositions[Random.Range(0, spawnPositions.Length)];
            
            PlayerController temp = null;
            if (model.isLocalPlayer)
            {
                temp = localPlayer;
            }
            else
            {
                if (freePlayers.Count > 0)
                    temp = freePlayers[0];
                freePlayers.Remove(temp);
                _busyPlayers.Add(model, temp);
            }
            
            temp.InjectModel(model);
            model.SetPosition(spwn.position);
            temp.SetPosition(spwn.position);
        }
    }
}
