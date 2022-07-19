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

        protected IIdentifiers identifiers;
        protected IControl control;
        protected IControlFactory controlFactory;
        protected IPlayersService playersService;

        public int TeamID { get => identifiers.TeamID; }
        public abstract ShipType ShipType { get; }
        protected bool isMobuleBusy;


        [Inject]
        private void Constructor(IControlFactory controlFactory, IPlayersService playersService)
        {
            this.controlFactory = controlFactory;
            this.playersService = playersService;
        }

        public bool IsAvailable()
        {
            return isMobuleBusy;
        }

        public ITargetable GetTarget()
        {
            return this;
        }

        public void NotifyTarget(Transform hunter)
        {
            //notify
        }

        public virtual void InitModule(IPlayer player)
        {
            identifiers = player.Identifiers;
            control = player.Control;
            isMobuleBusy = true;
        }


        public virtual void OnCreate()
        {
            //control = controlFactory.Get(identifiers);
        }

        public virtual void OnRestore()
        {
            //control = controlFactory.Get(identifiers);
        }

        public virtual void OnStore()
        {
            control.Disable();
        }

        public virtual void LeaveModule(IPlayer identifiers)
        {
            isMobuleBusy = false;
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
        jetFighter = 0,
        mothership = 1000
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

        public ControlEvent<RoutinePhase> OnDownPressed { get; }
        public ControlEvent<RoutinePhase> OnUpPressed { get; }

        public void Disable();
    }

    public interface IMothershipComponent
    {
        void SetAllModules(List<IModule> modules);
    }
}

