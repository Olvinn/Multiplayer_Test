using Data;
using Inputs;
using UnityEngine;

namespace Cameras
{
    public class CameraController : MonoBehaviour
    {
        public float positionLerpSpeed = 10f;
        public float horizontalSpeed = 1.5f, verticalSpeed = .75f;
        
        [SerializeField] private Camera camera;
        [SerializeField] private InputController input;
        
        public Vector3 positionOffset, targetOffset;

        private Transform _target;
        private Vector3 _savedAngle;
        private Vector3 _endPosition;

        private void Start()
        {
            input.rotation = Rotate;
        }

        private void Update()
        {
            camera.transform.position = Vector3.Slerp(camera.transform.position, _endPosition, Time.deltaTime * positionLerpSpeed);
        }

        private void LateUpdate()
        {
            Vector3 offset = camera.transform.localToWorldMatrix * targetOffset;
            camera.transform.LookAt(_target.position + offset);
        }

        public void SetSettings(CameraSettings cameraSettings)
        {
            positionLerpSpeed = cameraSettings.lerpSpeed;
            horizontalSpeed = cameraSettings.horizontalSpeed;
            verticalSpeed = cameraSettings.verticalSpeed;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void Rotate(Vector2 delta)
        {
            Debug.Log(delta);

            _savedAngle += new Vector3(-(delta.y * verticalSpeed) % 360, (delta.x * horizontalSpeed) % 360, 0);

            _endPosition = _target.transform.position + Quaternion.Euler(_savedAngle) * positionOffset;
        }
    }
}
