using System;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        // [SerializeField] private Transform actors;
        [SerializeField] private PlayerView playerPrefab;
        
        // [SerializeField] private PlayerController playerController;
        // [SerializeField] private CameraController cameraController;
        // [SerializeField] private InputController input;

        private int _currentScene = -1;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            // var view = Instantiate(playerPrefab, actors);
            // playerController.SetView(view);
            //
            // playerController.InjectModel(new PlayerModel());
            // playerController.SetSettings(GameContext.Instance.PlayerSettings);
            // playerController.OnSuccessfulDash = DashEffect;
            //
            // cameraController.SetTarget(view.transform);
            // cameraController.SetSettings(GameContext.Instance.CameraSettings);

            // input.movement = Move;
            // input.rotation = Rotate;
            // input.attack = Dash;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void LoadBase()
        {
            SceneManager.LoadSceneAsync(2);
        }

        public void LoadMainMenu()
        {
            if (_currentScene >= 0)
                SceneManager.UnloadSceneAsync(_currentScene);
            _currentScene = 1;
            SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Additive);
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        void Move(Vector2 dir)
        {
            // playerController.Movement(dir);
        }

        void Rotate(Vector2 delta)
        {
            // cameraController.Rotate(delta);
        }

        void Dash()
        {
            // playerController.Dash();
        }

        void DashEffect()
        {
            // cameraController.DashEffect(GameContext.Instance.PlayerSettings.dashSpeed);
        }
    }
}
