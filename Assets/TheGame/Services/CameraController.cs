using UnityEngine;
using Cinemachine;

namespace TheGame
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera playCamera;
        [SerializeField] private Canvas canvas;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        public Camera Camera => playCamera;

        public void Init(Transform toFollow, int totalPlayers, int playerN)
        {
            virtualCamera.Follow = toFollow;
            virtualCamera.LookAt = toFollow;

            var screenFactory = new ScreenFactory();
            var cameraSettings = screenFactory.Get(totalPlayers, playerN);

            var rect = playCamera.rect;
            rect.x = cameraSettings.x;
            playCamera.rect = rect;
            virtualCamera.gameObject.layer = cameraSettings.playerLayer;
            playCamera.cullingMask = cameraSettings.layerMask;
            canvas.gameObject.layer = cameraSettings.playerLayer;
        }

        public void SwitchTarget(Transform toFollow)
        {
            virtualCamera.Follow = toFollow;
            virtualCamera.LookAt = toFollow;
        }
    }

    public class PersonalCameraPreferences : MonoBehaviour
    {
        [field: SerializeField] public float OrthoSize { get; private set; }
    }
}

