using UnityEngine;

namespace Inputs
{
    public class OldInput : InputController
    {
        private void Update()
        {
            Vector2 mov = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (mov.magnitude > 1)
                mov.Normalize();
            movement?.Invoke(mov);

            Vector2 rot = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            rotation?.Invoke(rot);
            
            if (Input.GetButtonDown("Fire1"))
                attack?.Invoke();
        }
    }
}
