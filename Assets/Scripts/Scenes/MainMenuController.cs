using Cameras;
using Game;
using TMPro;
using UnityEngine;

namespace Scenes
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private CameraController camera;
        [SerializeField] private TMP_InputField input;
        
        void Start()
        {
            camera.Rotate(new Vector2(Random.Range(0, 360), 0));
            input.text = GameContext.Instance.name;
        }

        void Update()
        {
            camera.Rotate(new Vector2(Time.deltaTime * 10, 0));
        }

        public void StartHost()
        {
            GameController.Instance.Host();
        }

        public void StartClient()
        {
            GameController.Instance.Client();
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void SetName(string name)
        {
            GameContext.Instance.name = name;
        }
    }
}
