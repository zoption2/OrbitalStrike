using UnityEngine;

namespace TheGame
{
    public class Rotation : MonoBehaviour
    {
        private ControlEvent<Vector2, RoutinePhase> control;
        [SerializeField] private float angularSpeed;

        public void Init(ControlEvent<Vector2, RoutinePhase> control)
        {
            this.control = control;
            control.action += Rotate;
        }

        private void OnDisable()
        {
            control.action -= Rotate;
        }

        public void Rotate(Vector2 direction, RoutinePhase phase)
        {
            var dir = transform.rotation * Quaternion.FromToRotation(transform.up, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, dir, angularSpeed);
        }
    }
}

