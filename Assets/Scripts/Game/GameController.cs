using Cameras;
using Player;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CameraController cameraController;

        private void Start()
        {
            playerController.model = new PlayerModel();
            playerController.SetSettings(GameContext.Instance.playerSettings);
            
            cameraController.SetTarget(playerController.view.transform);
            cameraController.SetSettings(GameContext.Instance.cameraSettings);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
