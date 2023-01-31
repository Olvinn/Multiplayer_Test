using System.Collections;
using Data;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float dashDistance, dashSpeed, dashDelay;
        
        public Transform camera;

        PlayerModel _model;
        [SerializeField] PlayerView view;
        private bool _canDash = true;
        private bool _isClient;

        private void Update()
        {
            if (_model != null && !_isClient)
            {
                view.Move(_model.velocity);
            }
        }

        public void InjectModel(PlayerModel model)
        {
            _isClient = model.isLocalPlayer;
            _model = model;
            _model.onPositionChanged = view.SetPos;
            view.gameObject.SetActive(true);
        }

        public void SetSettings(PlayerSettings settings)
        {
            view.moveSpeed = settings.moveSpeed;

            dashDelay = settings.dashDelay;
            dashSpeed = settings.dashSpeed;
            dashDistance = settings.dashDistance;
        }

        public void SetView(PlayerView view)
        {
            this.view = view;
        }

        public void Dash()
        {
            if (!_canDash)
                return;
            
            Vector3 forwardVector = camera.forward;
            forwardVector.y = 0;
            forwardVector.Normalize();
            
            view.Dash(forwardVector, dashDistance, dashSpeed);

            StartCoroutine(DashDelay());
        }

        public void Movement(Vector2 direction)
        {
            Vector3 movDir = new Vector3(direction.x, 0, direction.y);

            Vector3 forwardVector = camera.forward;
            forwardVector.y = 0;
            forwardVector.Normalize();
            
            Quaternion ang = Quaternion.FromToRotation(Vector3.forward, forwardVector);

            var res = ang * movDir;
            
            view.Move(res);
            
            if (_model == null)
                return;
            
            _model.SetVelocity(res);
            _model.SetPosition(view.transform.position);
        }

        public void SetPosition(Vector3 pos)
        {
            view.SetPos(pos);
        }

        IEnumerator DashDelay()
        {
            _canDash = false;
            yield return new WaitForSeconds(dashDelay);
            _canDash = true;
        }
    }
}
