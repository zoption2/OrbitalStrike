using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGame
{
    public class IngameSettings : IGameSettings
    {
        public int TotalPlayers { get; set; } = 1;
        public SplitScreenPosition ScreenPosition { get; set; } = SplitScreenPosition.horizontal; 
    }

    public interface IGameSettings
    {
        int TotalPlayers { get; set; }
    }

    public enum SplitScreenPosition
    {
        horizontal,
        vertical
    }
}

