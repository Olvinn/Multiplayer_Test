using System;
using UnityEngine;

namespace Game
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameObject[] persistantObjects;

        private void Awake()
        {
            var temp = new GameObject("--- Persistant Objects ---");
            foreach (var p in persistantObjects)
            {
                p.transform.SetParent(temp.transform);
            }
            DontDestroyOnLoad(temp);
        }

        private void Start()
        {
            GameController.Instance.LoadMainMenu();
            
            Destroy(gameObject);
        }
    }
}
