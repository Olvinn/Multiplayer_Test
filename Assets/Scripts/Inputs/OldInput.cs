using UnityEngine;

namespace Inputs
{
    public class OldInput : InputController
    {
        private Vector2 _savedMousePosition;
        
        private void Update()
        {
            Vector2 mov = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            mov.Normalize();
            movement?.Invoke(mov);

            Vector2 rot = (Vector2)Input.mousePosition - _savedMousePosition;
            rotation?.Invoke(rot);
            _savedMousePosition = Input.mousePosition;
            
            if (Input.GetButtonDown("Fire1"))
                attack?.Invoke();
        }
    }
}
