using System.Collections;
using Cameras;
using Mirror;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        [SerializeField] private PlayerView playerPrefab;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            //
            //
            // cameraController.SetTarget(view.transform);
            // cameraController.SetSettings(GameContext.Instance.CameraSettings);

            // input.movement = Move;
            // input.rotation = Rotate;
            // input.attack = Dash;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void Host()
        {
            NetworkManager.singleton.StartHost();
        }

        public void RegisterPlayer(PlayerModel model)
        {
            StartCoroutine(SpawnPlayer(model));
        }

        IEnumerator SpawnPlayer(PlayerModel model)
        {
            yield return null;
            
            var temp = new GameObject("LocalPlayer");
            var cont = temp.AddComponent<PlayerController>();
            var view = Instantiate(playerPrefab);
            
            temp = new GameObject("PlayerCamera");
            var cam = temp.AddComponent<CameraController>();
            cam.camera = Camera.main;
            cam.SetTarget(view.transform);

            cont.cameraController = cam;
            cont.SetView(view);
            cont.InjectModel(model);
            cont.SetSettings(GameContext.Instance.PlayerSettings);
        }
    }
}
