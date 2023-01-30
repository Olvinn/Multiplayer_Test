using Cameras;
using UnityEngine;

namespace Scenes
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private CameraController camera;
        
        void Start()
        {
            camera.Rotate(new Vector2(Random.Range(0, 360), 0));
        }

        void Update()
        {
            camera.Rotate(new Vector2(Time.deltaTime * 10, 0));
        }
    }
}
