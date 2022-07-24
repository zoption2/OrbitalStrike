using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace TheGame
{
    public class MultiplayerUIController : MultiplayerEventSystem, IMultiplayerUI
    {
        public void SetFirstSelected(GameObject firstSelected)
        {
            firstSelectedGameObject = firstSelected;
        }

        public void SetPlayerPopup(GameObject popup)
        {
            playerRoot = popup;
        }
    }

    public interface IMultiplayerUI
    {
        void SetPlayerPopup(GameObject popup);
        void SetFirstSelected(GameObject firstSelected);
    }
}

