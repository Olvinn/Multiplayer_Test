using System;
using Mirror;
using Mirror.Discovery;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }

        public NetworkDiscovery networkDiscovery;

        public Action<PlayerController> onCreatePlayer;
        public Action<string> onServerFound;

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
            // Can't configure working client and network discovery right now
            // networkDiscovery.OnServerFound.AddListener(OnServerFound);
            // networkDiscovery.StartDiscovery();
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
#endif
        }

        public void Host(string addr)
        {
            CustomNetworkManager.singleton.networkAddress = addr;
            CustomNetworkManager.singleton.StartHost();
            networkDiscovery.AdvertiseServer();

#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
        }

        public void Client(string addr)
        {
            NetworkManager.singleton.networkAddress = addr;
            NetworkManager.singleton.StartClient();

#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
#endif
        }

        public void RegisterPlayer(PlayerController model)
        {
            onCreatePlayer?.Invoke(model);
        }

        public void Quit()
        {
            Application.Quit();
        }

        void OnServerFound(ServerResponse response)
        {
            onServerFound?.Invoke(response.EndPoint.Address.ToString());
            networkDiscovery.StopDiscovery();
        }
    }
}
