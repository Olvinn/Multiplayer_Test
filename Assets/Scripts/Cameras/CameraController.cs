using Inputs;
using UnityEngine;

namespace Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private InputController input;
        
        public Vector3 positionOffset, targetOffset;

        private Transform _target;

        private void Start()
        {
            input.rotation = Rotate;
        }

        private void LateUpdate()
        {
            Vector3 offset = camera.transform.localToWorldMatrix * targetOffset;
            camera.transform.LookAt(_target.position + offset);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Rotate(Vector2 delta)
        {
            
        }

        public Transform GetCameraTransform()
        {
            return camera.transform;
        }
    }
}
