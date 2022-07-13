using UnityEngine;

namespace TheGame
{
    public struct CameraSettings
    {
        public int player;
        public SplitScreenPosition splitPosition;
        public float x;
        public float y;
        public float h;
        public float w;
        public int playerLayer;
        public LayerMask layerMask;
    }

    public interface IScreenFactory
    {
        CameraSettings Get(int players, int currentPlayer, SplitScreenPosition splitScreen = SplitScreenPosition.horizontal);
    }

    public class ScreenFactory : IScreenFactory
    {
        public CameraSettings Get(int players, int currentPlayer, SplitScreenPosition splitScreen = SplitScreenPosition.horizontal)
        {
            CameraSettings settings = default;

            if (players == 1)
            {
                settings = new CameraSettings()
                {
                   player = 1, splitPosition = splitScreen, x =0, y = 0, h = 1, w = 1, playerLayer = 6, layerMask = 1<<0 | 1<<6
                };
            }
            else if (players == 2)
            {
                switch (currentPlayer)
                {
                    case 1:
                        settings = new CameraSettings()
                        {
                             player = 1, splitPosition = splitScreen, x =-0.5f, y = 0, h = 1, w = 1, playerLayer = 6, layerMask = 1<<0 | 1<<6
                        };
                        break;
                    case 2:
                        settings = new CameraSettings()
                        {
                             player = 2, splitPosition = splitScreen, x =0.5f, y = 0, h = 1, w = 1, playerLayer = 7, layerMask = 1<<0 | 1<<7
                        };
                        break;
                }
            }
            else if (players == 3)
            {
                switch (currentPlayer)
                {
                    case 1:
                        settings = new CameraSettings()
                        {
                             player = 1, splitPosition = splitScreen, x =-0.5f, y = -0.5f, h = 1, w = 1, playerLayer = 6, layerMask = 1<<0 | 1<<6
                        };
                        break;
                    case 2:
                        settings = new CameraSettings()
                        {
                             player = 2, splitPosition = splitScreen, x =0.5f, y = -0.5f, h = 1, w = 1, playerLayer = 7, layerMask = 1<<0 | 1<<7
                        };
                        break;
                    case 3:
                        settings = new CameraSettings()
                        {
                             player = 3, splitPosition = splitScreen, x =0f, y = 0.5f, h = 1, w = 1, playerLayer = 8, layerMask = 1<<0 | 1<<8
                        };
                        break;
                }
            }
            else if (players == 4)
            {
                switch (currentPlayer)
                {
                    case 1:
                        settings = new CameraSettings()
                        {
                             player = 1, splitPosition = splitScreen, x =-0.5f, y = -0.5f, h = 1, w = 1, playerLayer = 6, layerMask = 1<<0 | 1<<6
                        };
                        break;
                    case 2:
                        settings = new CameraSettings()
                        {
                             player = 2, splitPosition = splitScreen, x =0.5f, y = -0.5f, h = 1, w = 1, playerLayer = 7, layerMask = 1<<0 | 1<<7
                        };
                        break;
                    case 3:
                        settings = new CameraSettings()
                        {
                             player = 3, splitPosition = splitScreen, x =-0.5f, y = 0.5f, h = 1, w = 1, playerLayer = 8, layerMask = 1<<0 | 1<<8
                        };
                        break;
                    case 4:
                        settings = new CameraSettings()
                        {
                             player = 4, splitPosition = splitScreen, x =0.5f, y = 0.5f, h = 1, w = 1, playerLayer = 9, layerMask = 1<<0 | 1<<9
                        };
                        break;
                }
            }

            return settings;
        }
    }
}

