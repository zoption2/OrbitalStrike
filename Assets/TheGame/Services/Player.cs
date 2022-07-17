using UnityEngine;

namespace TheGame
{
    public class Player : MonoBehaviour
    {
        public CameraController cameraController;
        public IIdentifiers identifiers;
        public IControl control;
        public PlayerPersonalData personalData;

        public void Init(IIdentifiers identifiers)
        {
            this.identifiers = identifiers;
        }
    }
}

