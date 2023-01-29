using Inputs;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData model;
        [SerializeField] private PlayerView view;
        [SerializeField] private InputController input;

        private void Start()
        {
            input.attack = Attack;
            input.movement = Movement;
            input.rotation = Rotation;
        }

        public void Attack()
        {
            Debug.Log($"PlayerController: attack command received");
        }

        public void Movement(Vector2 direction)
        {
            //Debug.Log($"PlayerController: move command received with {direction} direction");
            view.Move(direction);
        }

        public void Rotation(Vector2 delta)
        {
            // Debug.Log($"PlayerController: rotate command received with {delta} changes");
        }
    }
}
