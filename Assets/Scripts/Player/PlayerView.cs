using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerView : MonoBehaviour
    {
        public Action<PlayerView> OnGetDashed;
        public float moveSpeed = 3f;
        public PlayerViewState State { get; private set; }
        
        [SerializeField] private CharacterController characterController;

        private Coroutine _dashCoroutine;

        private void Awake()
        {
            if(characterController == null)
                characterController = GetComponent<CharacterController>();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            switch (State)
            {
                case PlayerViewState.Dash:
                    var target = hit.collider.GetComponent<PlayerView>();
                    if (target != null)
                        target.GetDashed(this);
                    break;
            }
        }

        public void Move(Vector3 direction)
        {
            var mov = direction * (moveSpeed * Time.deltaTime);
            characterController.Move(mov);
        }

        public void Dash(Vector3 direction, float distance, float time)
        {
            State = PlayerViewState.Dash;
            
            if (_dashCoroutine != null)
                StopCoroutine(_dashCoroutine);
            
            _dashCoroutine = StartCoroutine(DashCoroutine(direction, distance, time));
        }

        public void GetDashed(PlayerView view)
        {
            OnGetDashed?.Invoke(view);
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
            State = PlayerViewState.Idle;
        }
    }

    public enum PlayerViewState
    {
        Idle,
        Dash
    }
}
