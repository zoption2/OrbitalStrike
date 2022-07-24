using UnityEngine;

namespace TheGame
{
    public abstract class MothershipModule : MonoBehaviour, IModule
    {
        [field: SerializeField] public ModuleInfo Info { get; private set; }
        [field: SerializeField] public CameraPrefs CameraPrefs { get; private set; }

        protected MotherShip motherShip;

        public void InitByMothership(MotherShip motherShip)
        {
            this.motherShip = motherShip;
        }

        public virtual void DisableControl(IPlayer player = null)
        {
            
        }

        public virtual void EnableControl(IPlayer player = null)
        {
            
        }

        public abstract bool IsAvailable();
        public abstract void JoinModule(IPlayer player);
        public abstract void LeaveModule(IPlayer player);


        public void OnCreate()
        {

        }

        public void OnRestore()
        {
            
        }

        public void OnStore()
        {
            
        }
    }
}

