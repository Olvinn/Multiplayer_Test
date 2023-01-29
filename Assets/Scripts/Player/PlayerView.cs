using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerView : MonoBehaviour
    {
        public float moveSpeed = 3f;
        
        [SerializeField] private CharacterController characterController;

        private void Awake()
        {
            if(characterController == null)
                characterController = GetComponent<CharacterController>();
        }

        public void Move(Vector3 direction)
        {
            var mov = direction * (moveSpeed * Time.deltaTime);
            characterController.Move(mov);
        }
    }
}
