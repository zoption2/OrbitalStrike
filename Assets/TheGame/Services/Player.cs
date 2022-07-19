using UnityEngine;

namespace TheGame
{
    public class Player : MonoBehaviour, IPlayer
    {
        [SerializeField] private CameraController cameraController;
        private IIdentifiers identifiers;
        private IControl control;
        private IModule module;
        public PlayerPersonalData personalData;

        public IIdentifiers Identifiers => identifiers;
        public ICameraControl CameraControl => cameraController;
        public IControl Control => control;
        public IModule Module { get => module; set => module = value; }

        public void SetIdentifiers(IIdentifiers identifiers)
        {
            this.identifiers = identifiers;
        }

        public void InitCameraController(Transform toFollow, int splitPlayers, int currentPlayerNo)
        {
            cameraController.InitCamera(toFollow, splitPlayers, currentPlayerNo);
        }

        public void SetControl(IControl control)
        {
            this.control = control;
        }

        public void SetModule(IModule module)
        {
            this.module = module;
        }
    }

    public interface IPlayer
    {
        IIdentifiers Identifiers { get; }
        ICameraControl CameraControl { get; }
        IControl Control { get; }
        IModule Module { get; set; }
    }
}

