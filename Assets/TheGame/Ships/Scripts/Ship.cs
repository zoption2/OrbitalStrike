using UnityEngine;
using System.Collections.Generic;
using TheGame.AI;
using Zenject;

namespace TheGame
{
    public abstract class Ship : MonoBehaviour, ITargetable, IModule
    {
        [SerializeField] protected Health health;
        [SerializeField] protected Rotation rotation;
        [field: SerializeField] public ModuleInfo Info { get; private set; }
        [field: SerializeField] public CameraPrefs CameraPrefs { get; private set; }

        protected IIdentifiers identifiers;
        protected IControl control;
        protected IControlFactory controlFactory;
        protected IPlayersService playersService;

        public int TeamID { get => identifiers.TeamID; }
        public abstract ShipType ShipType { get; }

        private bool isControlActive;
        private bool isModuleAvailable;


        [Inject]
        private void Constructor(IControlFactory controlFactory, IPlayersService playersService)
        {
            this.controlFactory = controlFactory;
            this.playersService = playersService;
        }

        public virtual bool IsAvailable()
        {
            return isModuleAvailable;
        }

        public ITargetable GetTarget()
        {
            return this;
        }

        public void NotifyTarget(Transform hunter)
        {
            //notify
        }

        public virtual void JoinModule(IPlayer player)
        {
            identifiers = player.Identifiers;
            control = player.Control;
            EnableControl();
        }


        public virtual void OnCreate()
        {
            isModuleAvailable = true;
        }

        public virtual void OnRestore()
        {
            isModuleAvailable = true;
        }

        public virtual void OnStore()
        {
            
        }

        public virtual void LeaveModule(IPlayer identifiers)
        {
            DisableControl();
            isModuleAvailable = true;
        }

        public virtual void EnableControl(IPlayer player = null)
        {
            isControlActive = true;
        }

        public virtual void DisableControl(IPlayer player = null)
        {
            isControlActive = false;
        }
    }

    public enum PlayerType
    {
        human,
        ai
    }

    public enum ShipType
    {
        jetFighter = 0,
        mothership = 1000
    }


    public interface IControl
    {
        public ControlEvent<Vector2, RoutinePhase> OnLeftStickUse { get;}
        public ControlEvent<Vector2, RoutinePhase> OnRightStickUse { get;}
        public ControlEvent<RoutinePhase> OnRightTriggerUse { get;}
        public ControlEvent<RoutinePhase> OnAButton { get;}
        public ControlEvent<RoutinePhase> OnYButton { get;}
        public ControlEvent<RoutinePhase> OnBButton { get;}
        public ControlEvent<RoutinePhase> OnXButton { get;}
        public ControlEvent<RoutinePhase> OnRightShoulder { get;}

        public ControlEvent<Vector2, RoutinePhase> OnMoveDPad { get; }

        public void ChangeActionMap(ActionMapType actionMap);
    }

    public interface IControlled
    {
        void EnableControl(IPlayer player = null);
        void DisableControl(IPlayer player = null);
    }
}

