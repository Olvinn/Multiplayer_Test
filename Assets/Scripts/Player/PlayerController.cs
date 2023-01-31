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
        public int points;
        
        public Transform camera;

        [SerializeField] private CharacterController characterController;
        [SerializeField] private MeshRenderer renderer;

        private PlayerViewState _state;
        private PlayerSettings _settings;
        private Coroutine _dashCoroutine;
        private bool _canDash = true, _invulnerable = false;
        private Color _color;

        private void Start()
        { 
            GameController.Instance.RegisterPlayer(this);
            if (isLocalPlayer)
                _color = Color.blue;
            else
                _color = Color.red;
            renderer.material.color = _color;
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            switch (_state)
            {
                case PlayerViewState.Dash:
                    var target = hit.collider.GetComponent<PlayerController>();
                    if (target != null)
                        target.GetDashed(this);
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

        private void GetDashed(PlayerController player)
        {
            if (_invulnerable)
                return;

            CmdApplyDashedEffect();
            CmdIncreasePoints(player);
        }

        [Command(requiresAuthority = false)]
        private void CmdIncreasePoints(PlayerController player)
        {
            Debug.Log($"CmdIncreasePoints {player}");
            IncreasePoints(player);
        }

        [Command(requiresAuthority = false)]
        private void CmdApplyDashedEffect()
        {
            Debug.Log($"CmdApplyDashedEffect");
            ApplyDashedEffect();
        }

        [ClientRpc]
        private void IncreasePoints(PlayerController player)
        {
            Debug.Log($"IncreasePoints {player}");
            player.points++;
        }

        [ClientRpc]
        private void ApplyDashedEffect()
        {
            Debug.Log($"ApplyDashedEffect");
            StartCoroutine(Invulnerability());
            renderer.material.color = Color.black;
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

        IEnumerator Invulnerability()
        {
            _invulnerable = true;
            yield return new WaitForSeconds(_settings.invulnarabilityDelay);
            renderer.material.color = _color;
            _invulnerable = false;
        }
    }

    public enum PlayerViewState
    {
        Idle,
        Dash
    }
}
