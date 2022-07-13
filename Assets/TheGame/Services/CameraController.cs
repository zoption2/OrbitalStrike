using UnityEngine;
using Cinemachine;

namespace TheGame
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera playCamera;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        public void Init(Transform toFollow, int totalPlayers, int playerN)
        {
            virtualCamera.Follow = toFollow;

            var screenFactory = new ScreenFactory();
            var cameraSettings = screenFactory.Get(totalPlayers, playerN);

            var rect = playCamera.rect;
            rect.x = cameraSettings.x;
            playCamera.rect = rect;
            virtualCamera.gameObject.layer = cameraSettings.playerLayer;
            playCamera.cullingMask = cameraSettings.layerMask;
        }
    }

    public class PersonalCameraPreferences : MonoBehaviour
    {
        [field: SerializeField] public float OrthoSize { get; private set; }
    }
}

