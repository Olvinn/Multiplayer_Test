using System;
using System.Collections;
using Data;
using Game;
using Mirror;
using UnityEngine;

namespace Player
{
    public class PlayerController : NetworkBehaviour
    {
        public Transform camera;

        [SerializeField] private CharacterController characterController;

        private PlayerViewState _state;
        private PlayerSettings _settings;
        private Coroutine _dashCoroutine;
        private bool _canDash = true;

        private void Start()
        {
            GameController.Instance.RegisterPlayer(this);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            switch (_state)
            {
                case PlayerViewState.Dash:
                    var target = hit.collider.GetComponent<PlayerController>();
                    // if (target != null)
                    //     target.GetDashed(this);
                    break;
            }
        }
        public void SetSettings(PlayerSettings settings)
        {
            _settings = settings;
        }

        public void Dash()
        {
            if (!_canDash)
                return;
            
            Vector3 forwardVector = camera.forward;
            forwardVector.y = 0;
            forwardVector.Normalize();
            
            Dash(forwardVector, _settings.dashDistance, _settings.dashSpeed);

            StartCoroutine(DashDelay());
        }

        public void Movement(Vector2 direction)
        {
            if (_state == PlayerViewState.Dash)
                return;
            
            Vector3 movDir = new Vector3(direction.x, 0, direction.y);

            Vector3 forwardVector = camera.forward;
            forwardVector.y = 0;
            forwardVector.Normalize();
            
            Quaternion ang = Quaternion.FromToRotation(Vector3.forward, forwardVector);

            var res = ang * movDir;
            
            Move(res);
        }

        private void Move(Vector3 direction)
        {
            var mov = direction * (_settings.moveSpeed * Time.deltaTime);
            characterController.Move(mov);
        }

        public void Dash(Vector3 direction, float distance, float time)
        {
            _state = PlayerViewState.Dash;
            
            if (_dashCoroutine != null)
                StopCoroutine(_dashCoroutine);
            
            _dashCoroutine = StartCoroutine(DashCoroutine(direction, distance, time));
        }

        IEnumerator DashDelay()
        {
            _canDash = false;
            yield return new WaitForSeconds(_settings.dashDelay);
            _canDash = true;
        }
        
        IEnumerator DashCoroutine(Vector3 direction, float distance, float time)
        {
            float timer = time;
            while (timer > 0)
            {
                characterController.Move(direction * (distance / time * Time.deltaTime));
                timer -= Time.deltaTime;
                yield return null;
            }
            _state = PlayerViewState.Idle;
        }
    }

    public enum PlayerViewState
    {
        Idle,
        Dash
    }
}
