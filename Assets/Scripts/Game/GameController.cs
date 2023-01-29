using Cameras;
using Inputs;
using Player;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform actors;
        [SerializeField] private PlayerView playerPrefab;
        
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private InputController input;

        private void Start()
        {
            var view = Instantiate(playerPrefab, actors);
            playerController.SetView(view);
            
            playerController.InjectModel(new PlayerModel());
            playerController.SetSettings(GameContext.Instance.PlayerSettings);
            playerController.OnSuccessfulDash = DashEffect;
            
            cameraController.SetTarget(view.transform);
            cameraController.SetSettings(GameContext.Instance.CameraSettings);

            input.movement = Move;
            input.rotation = Rotate;
            input.attack = Dash;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Move(Vector2 dir)
        {
            playerController.Movement(dir);
        }

        void Rotate(Vector2 delta)
        {
            cameraController.Rotate(delta);
        }

        void Dash()
        {
            playerController.Dash();
        }

        void DashEffect()
        {
            cameraController.DashEffect(GameContext.Instance.PlayerSettings.dashSpeed);
        }
    }
}
