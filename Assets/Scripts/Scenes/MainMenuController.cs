using Cameras;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Scenes
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private CameraController camera;
        [SerializeField] private TMP_InputField input;
        [SerializeField] private TextMeshProUGUI messages;
        [SerializeField] private TMP_InputField hostIP, clientIP;
        [SerializeField] private Button connectButton;
        
        void Start()
        {
            camera.Rotate(new Vector2(Random.Range(0, 360), 0));
            input.text = GameContext.Instance.playerName;
            hostIP.text = GameContext.Instance.GameSettings.HostAddress;
            clientIP.text = GameContext.Instance.GameSettings.HostAddress;
            // connectButton.interactable = false;

            GameController.Instance.onServerFound = OnFoundHost;
        }

        void Update()
        {
            camera.Rotate(new Vector2(Time.deltaTime * 10, 0));
        }

        public void StartHost()
        {
            GameController.Instance.Host(hostIP.text);
        }

        public void StartClient()
        {
            GameController.Instance.Client(clientIP.text);
        }

        public void Quit()
        {
            GameController.Instance.Quit();
        }

        public void SetName(string name)
        {
            GameContext.Instance.playerName = name;
        }

        public void SetHostToConnect(string name)
        {
            messages.text = "Searching for host...";
            connectButton.interactable = true;
        }

        private void OnFoundHost(string msg)
        {
            messages.text = $"Host {msg} found!";
            connectButton.interactable = true;
            clientIP.text = msg;
        }
    }
}
