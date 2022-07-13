using UnityEngine;
using System;
using TheGame.AI;
using Zenject;

namespace TheGame
{
    public abstract class Ship : MonoBehaviour, ITargetable, IShip, IModule
    {
        [SerializeField] protected Health health;
        [SerializeField] protected Rotation rotation;

        protected IIdentifiers identifiers;
        protected IControl control;
        protected IControlFactory controlFactory;
        protected IPlayersService playersService;

        public int TeamID { get => identifiers.TeamID; }
        public abstract ShipType ShipType { get; }
        public bool IsBusy { get; private set; }


        [Inject]
        private void Constructor(IControlFactory controlFactory, IPlayersService playersService)
        {
            this.controlFactory = controlFactory;
            this.playersService = playersService;
        }

        public ITargetable GetTarget()
        {
            return this;
        }

        public void NotifyTarget(Transform hunter)
        {
            //notify
        }

        public virtual void Initialize(IIdentifiers identifiers, IControl control)
        {
            this.identifiers = identifiers;
            this.control = control;
        }

        public void OnCreate()
        {
            //control = controlFactory.Get(identifiers);
        }

        public void OnRestore()
        {
            //control = controlFactory.Get(identifiers);
        }

        public void OnStore()
        {
            control.Disable();
        }

        public bool TryGetModule(int id)
        {
            if (IsBusy)
            {
                return false;
            }
            IsBusy = true;
            return true;
        }

        public void LeaveModule()
        {
            IsBusy = false;
        }
    }

    public interface IIdentifiers
    {
        int ID { get; }
        int TeamID { get; }
        PlayerType PlayerType { get; }
    }

    public struct Identifiers : IIdentifiers
    {
        public int ID { get; private set; }
        public int TeamID { get; private set; }
        public PlayerType PlayerType { get; private set; }

        public Identifiers(int id, int teamID, PlayerType playerType = PlayerType.human)
        {
            ID = id;
            TeamID = teamID;
            PlayerType = playerType;
        }
    }

    public enum PlayerType
    {
        human,
        ai
    }

    public enum ShipType
    {
        jetFighter = 0
    }


    public interface IControl
    {
        public ControlEvent<Vector2, RoutinePhase> OnRotation { get;}
        public ControlEvent<Vector2, RoutinePhase> OnAim { get;}
        public ControlEvent<RoutinePhase> OnPrimeWeapon { get;}
        public ControlEvent<RoutinePhase> OnSecondaryWeapon { get;}
        public ControlEvent<RoutinePhase> OnAdvanced { get;}
        public ControlEvent<RoutinePhase> OnTurbo { get;}
        public ControlEvent<RoutinePhase> OnBreaks { get;}
        public ControlEvent<RoutinePhase> OnTraps { get;}

        public void Disable();
    }
}

