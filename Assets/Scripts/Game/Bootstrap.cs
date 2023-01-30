using UnityEngine;

namespace Game
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            GameController.Instance.LoadBase();
            GameController.Instance.LoadMainMenu();
        }
    }
}
