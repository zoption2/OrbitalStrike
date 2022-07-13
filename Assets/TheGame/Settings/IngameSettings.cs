using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGame
{
    [CreateAssetMenu(fileName = "IngameSettings", menuName = "ScriptableObjects/IngameSettings")]
    public class IngameSettings : ScriptableObject
    {
        [field: SerializeField] public int Players { get; set; } = 1;
        [field: SerializeField] public SplitScreenPosition ScreenPosition { get; set; } = SplitScreenPosition.horizontal; 
    }

    public enum SplitScreenPosition
    {
        horizontal,
        vertical
    }
}

