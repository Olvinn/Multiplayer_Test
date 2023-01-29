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
            playerController.InjectModel(new PlayerModel());
            playerController.SetSettings(GameContext.Instance.PlayerSettings);
            
            cameraController.SetTarget(playerController.view.transform);
            cameraController.SetSettings(GameContext.Instance.CameraSettings);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
