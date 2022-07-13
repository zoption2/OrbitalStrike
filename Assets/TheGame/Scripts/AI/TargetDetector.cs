using System.Collections.Generic;
using UnityEngine;


namespace TheGame.AI
{
    public class TargetDetector : MonoBehaviour, IGlobalFixedUpdateObserver
    {
        private IGlobalFixedUpdateProvider fixedUpdateProvider;
        private ITargetable target;
        private IIdentifiers identifiers;
        private List<ITargetable> allTargets = new List<ITargetable>();
        
        public System.Action<ITargetable> OnTargetDetect;
        public System.Action OnNoTarget;


        public void Init(IIdentifiers identifiers, IGlobalFixedUpdateProvider fixedUpdateProvider)
        {
            this.identifiers = identifiers;
            this.fixedUpdateProvider = fixedUpdateProvider;
            fixedUpdateProvider.Add(this);
        }

        public void Release()
        {
            fixedUpdateProvider.Remove(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ITargetable>(out ITargetable targetable))
            {
                if (targetable.TeamID != identifiers.TeamID)
                {
                    allTargets.Add(targetable);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ITargetable>(out ITargetable targetable))
            {
                if (allTargets.Contains(targetable))
                {
                    allTargets.Remove(targetable);
                }
            }
        }

        private void DoOnTargetLost(ITargetable targetable)
        {
            if (allTargets.Contains(targetable))
            {
                allTargets.Remove(targetable);
            }
        }

        private void Detect()
        {
            if (allTargets.Count == 0)
            {
                OnNoTarget?.Invoke();
                return;
            }

            target = allTargets[0];
            float distance = Vector2.Distance(transform.position, allTargets[0].transform.position);
            for (int i = 1, j = allTargets.Count; i < j; i++)
            {
                float dist = Vector2.Distance(transform.position, allTargets[i].transform.position);
                if (dist < distance)
                {
                    target = allTargets[i];
                    distance = dist;
                }
            }
            OnTargetDetect?.Invoke(target);
            target.NotifyTarget(transform);
        }

        public void FixedUpdateGlobal()
        {
            Detect();
        }
    }

    public interface ITargetable
    {
        public int TeamID { get; }
        public Transform transform { get; }
        public ITargetable GetTarget();
        public void NotifyTarget(Transform hunter);
    }
}

