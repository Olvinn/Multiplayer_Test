using System;
using UnityEngine;

namespace Inputs
{
    public abstract class InputController : MonoBehaviour
    {
        public Action<Vector2> movement;
        public Action<Vector2> rotation;
        public Action attack;
    }
}
