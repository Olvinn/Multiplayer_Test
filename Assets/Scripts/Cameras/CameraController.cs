using System.Collections;
using Data;
using UnityEngine;

namespace Cameras
{
    public class CameraController : MonoBehaviour
    {
        public float positionLerpSpeed = 10f;
        public float horizontalSpeed = 1.5f,
            verticalSpeed = .75f;
        public AnimationCurve dashEffectZoom;
        public float fieldOfView = 60;
        
        [SerializeField] private Camera camera;
        [SerializeField] private Transform target;
        
        public Vector3 positionOffset, targetOffset;

        private Vector3 _savedAngle;
        private Vector3 _endPosition;

        private void Update()
        {
            camera.transform.position = Vector3.Slerp(camera.transform.position, _endPosition, Time.deltaTime * positionLerpSpeed);
        }

        private void LateUpdate()
        {
            Vector3 offset = camera.transform.localToWorldMatrix * targetOffset;
            camera.transform.LookAt(target.position + offset);
        }

        public void SetSettings(CameraSettings cameraSettings)
        {
            positionLerpSpeed = cameraSettings.LerpSpeed;
            horizontalSpeed = cameraSettings.HorizontalSpeed;
            verticalSpeed = cameraSettings.VerticalSpeed;
            fieldOfView = cameraSettings.FieldOfView;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void Rotate(Vector2 delta)
        {
            _savedAngle += new Vector3(-(delta.y * verticalSpeed) % 360, (delta.x * horizontalSpeed) % 360, 0);

            _endPosition = target.transform.position + Quaternion.Euler(_savedAngle) * positionOffset;
        }

        public void DashEffect(float speed)
        {
            StartCoroutine(ZoomIn(speed));
        }

        IEnumerator ZoomIn(float speed)
        {
            float timer = 0;
            while (timer < speed)
            {
                camera.fieldOfView = fieldOfView * dashEffectZoom.Evaluate(timer / speed);
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
