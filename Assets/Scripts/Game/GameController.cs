using System;
using Mirror;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public Action<PlayerModel> onCreatePlayer;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
#endif
        }

        public void Host()
        {
            NetworkManager.singleton.StartHost();

#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
        }

        public void Client()
        {
            NetworkManager.singleton.StartClient();

#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
        }

        public void RegisterPlayer(PlayerModel model)
        {
            onCreatePlayer?.Invoke(model);
        }
    }
}
