using System.Collections;
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

        public void Dash(Vector3 direction, float distance, float time)
        {
            StartCoroutine(DashCoroutine(direction, distance, time));
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
        }
    }
}
