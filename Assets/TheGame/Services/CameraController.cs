using UnityEngine;
using Cinemachine;
using System.Collections;


namespace TheGame
{
    public class CameraController : MonoBehaviour, ICameraControl
    {
        [SerializeField] private Camera playCamera;
        [SerializeField] private Canvas canvas;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;

        public Camera Camera => playCamera;
        public int Layer { get; private set; }

        public void InitCamera(Transform toFollow, int totalPlayers, int playerN)
        {
            virtualCamera.Follow = toFollow;
            virtualCamera.LookAt = toFollow;

            var screenFactory = new ScreenFactory();
            var cameraSettings = screenFactory.Get(totalPlayers, playerN);

            var rect = playCamera.rect;
            rect.x = cameraSettings.x;
            playCamera.rect = rect;
            Layer = cameraSettings.playerLayer;
            virtualCamera.gameObject.layer = cameraSettings.playerLayer;
            playCamera.cullingMask = cameraSettings.layerMask;
            canvas.gameObject.layer = cameraSettings.playerLayer;
        }

        public void ChangeCameraTarget(Transform toFollow, CameraPrefs cameraPrefs = null)
        {
            StartCoroutine(ChangeTargetRoutine(toFollow, cameraPrefs));
        }

        private WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
        
        private IEnumerator ChangeTargetRoutine(Transform toFollow, CameraPrefs cameraPrefs)
        {
            float time = 0f;
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            Vector3 previousPos = transform.position;
            Vector3 targetPos = toFollow.position;

            while (time < 1)
            {
                var currentPosition = Vector3.Lerp(previousPos, targetPos, time);
                virtualCamera.transform.position = currentPosition;
                time += Time.deltaTime;
                yield return waitFrame;
            }
            virtualCamera.Follow = toFollow;
            virtualCamera.LookAt = toFollow;

            if (cameraPrefs != null)
            {
                virtualCamera.m_Lens.OrthographicSize = cameraPrefs.OrthoSize;
            }
        }
    }

    public interface ICameraControl
    {
        Camera Camera { get; }
        int Layer { get; }
        void ChangeCameraTarget(Transform toFollow, CameraPrefs cameraPrefs = null);
    }

    public class PersonalCameraPreferences : MonoBehaviour
    {
        [field: SerializeField] public float OrthoSize { get; private set; }
    }
}

