using UnityEngine;
using TheGame.AI;
using Zenject;

namespace TheGame
{
    public class RocketRotation : MonoBehaviour, IGlobalFixedUpdateObserver
    {
        [SerializeField] private float angularSpeed;
        [SerializeField] private TargetDetector detector;
        [Inject] private IGlobalFixedUpdateProvider fixedUpdateProvider;
        private Vector2 lastTargetPosition;

        private void OnEnable()
        {
            fixedUpdateProvider.Add(this);
            detector.OnTargetDetect += CorrectDirection;
        }

        private void OnDisable()
        {
            fixedUpdateProvider.Remove(this);
            detector.OnTargetDetect -= CorrectDirection;
            lastTargetPosition = default;
        }

        private void CorrectDirection(ITargetable targetable)
        {
            lastTargetPosition = targetable.transform.position - transform.position;
        }

        public void Rotate(Vector2 direction)
        {
            var dir = transform.rotation * Quaternion.FromToRotation(transform.up, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, dir, angularSpeed);
        }

        public void FixedUpdateGlobal()
        {
            Rotate(lastTargetPosition);
        }
    }
}

