using System;
using System.Collections;
using Data;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public Action OnSuccessfulDash;
        public float dashDistance, dashSpeed, dashDelay;
        
        [SerializeField] private Transform playerCamera;

        PlayerModel _model;
        PlayerView _view;
        private bool _isUpdatingModel;
        private bool _canDash;
        
        private void Start()
        {
            _canDash = true;
        }

        public void InjectModel(PlayerModel model)
        {
            _model = model;
            model.onPositionChanged += SetPosition;
        }

        public void SetSettings(PlayerSettings settings)
        {
            _view.moveSpeed = settings.moveSpeed;

            dashDelay = settings.dashDelay;
            dashSpeed = settings.dashSpeed;
            dashDistance = settings.dashDistance;
        }

        public void SetView(PlayerView view)
        {
            _view = view;
        }

        public void Dash()
        {
            if (!_canDash)
                return;
            
            Vector3 forwardVector = playerCamera.forward;
            forwardVector.y = 0;
            forwardVector.Normalize();
            
            _view.Dash(forwardVector, dashDistance, dashSpeed);

            StartCoroutine(DashDelay());
            
            OnSuccessfulDash?.Invoke();
        }

        public void Movement(Vector2 direction)
        {
            Vector3 movDir = new Vector3(direction.x, 0, direction.y);

            Vector3 forwardVector = playerCamera.forward;
            forwardVector.y = 0;
            forwardVector.Normalize();
            
            Quaternion ang = Quaternion.FromToRotation(Vector3.forward, forwardVector);
            
            _view.Move(ang * movDir);
            
            SafelyUpdateModelPosition(_view.transform.position);
        }

        public void SetPosition(Vector3 position)
        {
            if (_isUpdatingModel)
                return;

            _view.transform.position = position;
            SafelyUpdateModelPosition(position);
        }
        
        private void SafelyUpdateModelPosition(Vector3 position)
        {
            _isUpdatingModel = true;
            _model.position = position;
            _isUpdatingModel = false;
        }

        IEnumerator DashDelay()
        {
            _canDash = false;
            yield return new WaitForSeconds(dashDelay);
            _canDash = true;
        }
    }
}
