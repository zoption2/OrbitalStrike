using System.Collections;
using System;
using Zenject;

namespace TheGame
{
    public class ControlledRoutine
    {
        private bool isWorking;
        private Action<RoutinePhase> OnPerformed;
        [Inject] private IMonoInstantiator mono;

        private UnityEngine.WaitForEndOfFrame waitFor = new UnityEngine.WaitForEndOfFrame();

        public ControlledRoutine(Action<RoutinePhase> onPerformed)
        {
            OnPerformed = onPerformed;
        }

        public void Start()
        {
            isWorking = true;
            mono.StartCoroutine(Routine());
        }

        public void Cancel()
        {
            isWorking = false;
        }

        private IEnumerator Routine()
        {
            OnPerformed?.Invoke(RoutinePhase.started);
            while (isWorking)
            {
                OnPerformed?.Invoke(RoutinePhase.perfomed);
                yield return waitFor;
            }
            OnPerformed?.Invoke(RoutinePhase.complete);
        }

        public class Factory : PlaceholderFactory<Action<RoutinePhase>, ControlledRoutine>
        {

        }
    }

    public enum RoutinePhase
    {
        started,
        perfomed,
        complete
    }
}

