using Data;
using Inputs;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerModel model;
        public PlayerView view;
        [SerializeField] private Transform playerCamera;
        [SerializeField] private InputController input;

        private bool isUpdatingModel;
        
        private void Start()
        {
            if (input == null)
                return;
            
            input.attack = Attack;
            input.movement = Movement;
        }

        public void InjectModel(PlayerModel model)
        {
            this.model = model;
            model.onPositionChanged += SetPosition;
        }

        public void SetSettings(PlayerSettings settings)
        {
            view.moveSpeed = settings.moveSpeed;
        }

        public void Attack()
        {
            // Debug.Log($"PlayerController: attack command received");
            
            Vector3 forwardVector = playerCamera.forward;
            forwardVector.y = 0;
            forwardVector.Normalize();
            
            view.Dash(forwardVector, 5, .1f);
        }

        public void Movement(Vector2 direction)
        {
            //Debug.Log($"PlayerController: move command received with {direction} direction");

            Vector3 movDir = new Vector3(direction.x, 0, direction.y);

            Vector3 forwardVector = playerCamera.forward;
            forwardVector.y = 0;
            forwardVector.Normalize();
            
            Quaternion ang = Quaternion.FromToRotation(Vector3.forward, forwardVector);
            
            view.Move(ang * movDir);
            
            SafelyUpdateModelPosition(view.transform.position);
        }

        public void SetPosition(Vector3 position)
        {
            if (isUpdatingModel)
                return;

            view.transform.position = position;
            SafelyUpdateModelPosition(position);
        }
        
        private void SafelyUpdateModelPosition(Vector3 position)
        {
            isUpdatingModel = true;
            model.position = position;
            isUpdatingModel = false;
        }
    }
}
