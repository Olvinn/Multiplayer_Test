using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Game/Settings/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [field: SerializeField] public string HostAddress { get; private set; } = "localhost";
        [field: SerializeField] public string[] PlayerNames { get; private set; } = { "The Chosen One", "One punch Man", 
            "Tester 3000", "Super Mario", "Spider-girl", "Kinky-Slime", "Bonk", "Mebeeb", "Hazard", "Common Player"};
    }
}
