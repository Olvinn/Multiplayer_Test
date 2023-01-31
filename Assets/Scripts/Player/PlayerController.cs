using System;
using System.Collections;
using Data;
using Game;
using Mirror;
using UnityEngine;

namespace Player
{
    // Main and the only one class, managing network logic
    // Should be split, but I don't sure I will
    public class PlayerController : NetworkBehaviour
    {
        public Action<string> onShowWinner;
        
        [SyncVar]
        public int points;
        [SyncVar]
        public string playerName;
        
        public Transform camera;

        [SerializeField] private CharacterController characterController;
        [SerializeField] private MeshRenderer renderer;

        private PlayerViewState _state;
        private PlayerSettings _settings;
        private Coroutine _dashCoroutine;
        private bool _canDash = true, _invulnerable = false;
        private Color _color;
        
        // Why don't use #regions? - I don't like them
        // ========= Both client and server logic ========
        private void Start()
        { 
            if (isLocalPlayer)
                _color = Color.blue;
            else
                _color = Color.red;
            renderer.material.color = _color;
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            GameController.Instance.RegisterPlayer(this);
            if (isLocalPlayer)
                UpdateName(GameContext.Instance.name);
        }

        public void SetSettings(PlayerSettings settings)
        {
            _settings = settings;
        }
        // =========================================================

        // ================ Client only logic ======================
        // Actually, these methods could be used on server, but due 
        // to smallness of game, it manages by upper logic
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!isLocalPlayer)
                return;
            
            switch (_state)
            {
                case PlayerViewState.Dash:
                    var target = hit.collider.GetComponent<PlayerController>();
                    if (target != null)
                        target.GetDashed(this);
                    break;
            }
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

        IEnumerator Invulnerability()
        {
            yield return new WaitForSeconds(_settings.invulnarabilityDelay);
            renderer.material.color = _color;
            _invulnerable = false;
        }
        // ================================================

        // ============ Command to server =================
        [Command(requiresAuthority = false)]
        private void GetDashed(PlayerController player)
        {
            if (_invulnerable)
                return;
            _invulnerable = true;
            player.points++;
            if (player.points == 3)
            {
                ShowResults(player.playerName);
                StartCoroutine(RestartTimer(5));
            }

            ApplyDashedEffect();
        }

        [Command(requiresAuthority = false)]
        private void UpdateName(string name)
        {
            ApplyName(name);
        }
        
        IEnumerator RestartTimer(float timer)
        {
            yield return new WaitForSeconds(timer);
            NetworkManager.singleton.ServerChangeScene(NetworkManager.singleton.onlineScene);
        }
        // ===============================================

        // ============= Server responses ================
        [ClientRpc]
        private void ApplyDashedEffect()
        {
            StartCoroutine(Invulnerability());
            renderer.material.color = Color.black;
        }

        [ClientRpc]
        private void ApplyName(string name)
        {
            playerName = name;
        }

        [ClientRpc]
        private void ShowResults(string winner)
        {
            onShowWinner?.Invoke(winner);
        }
        // ===============================================
    }

    public enum PlayerViewState
    {
        Idle,
        Dash
    }
}
