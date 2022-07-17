using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

namespace TheGame
{
    public class Control : IControl
    {
        private int id;
        private Gamepad gamepad;
        private ControlActions input;
        private PlayerInput playerInput;

        private ControlledRoutine controlledRotation;
        private ControlledRoutine controlledAim;
        private ControlledRoutine controlledSecondaryWeapon;
        private ControlledRoutine controlledPrimeWeapon;
        private ControlledRoutine controlledAdvanced;
        private ControlledRoutine controlledTurbo;
        private ControlledRoutine controlledBreaks;
        private ControlledRoutine controlledTraps;

        private ControlledRoutine controlledDownPressed;
        private ControlledRoutine controlledUpPressed;

        private ControlledRoutine.Factory factory;

        public ControlEvent<Vector2, RoutinePhase> OnRotation { get; } = new ControlEvent<Vector2, RoutinePhase>();
        public ControlEvent<Vector2, RoutinePhase> OnAim { get; } = new ControlEvent<Vector2, RoutinePhase>();
        public ControlEvent<RoutinePhase> OnPrimeWeapon { get; } = new ControlEvent<RoutinePhase>();
        public ControlEvent<RoutinePhase> OnSecondaryWeapon { get; } = new ControlEvent<RoutinePhase>();
        public ControlEvent<RoutinePhase> OnAdvanced { get; } = new ControlEvent<RoutinePhase>();
        public ControlEvent<RoutinePhase> OnTurbo { get; } = new ControlEvent<RoutinePhase>();
        public ControlEvent<RoutinePhase> OnBreaks { get; } = new ControlEvent<RoutinePhase>();
        public ControlEvent<RoutinePhase> OnTraps { get; } = new ControlEvent<RoutinePhase>();

        public ControlEvent<RoutinePhase> OnUpPressed { get; } = new ControlEvent<RoutinePhase>();
        public ControlEvent<RoutinePhase> OnDownPressed { get; } = new ControlEvent<RoutinePhase>();


        public Control(int id, ControlledRoutine.Factory factory)
        {
            this.id = id;
            this.factory = factory;
            var controllers = Gamepad.all.Count;
            if (id < controllers)
            {
                gamepad = Gamepad.all[id];
                playerInput = PlayerInput.FindFirstPairedToDevice(gamepad);
                playerInput.ActivateInput();
                input = new ControlActions(playerInput);
                input.SubscribeEvents();

                controlledRotation = factory.Create(DoRotation);
                controlledAim = factory.Create(DoAim);
                controlledPrimeWeapon = factory.Create(UsePrimeWeapon);
                controlledSecondaryWeapon = factory.Create(UseSecondaryWeapon);
                controlledAdvanced = factory.Create(UseAdvanced);
                controlledBreaks = factory.Create(UseBreaks);
                controlledTraps = factory.Create(UseTraps);
                controlledTurbo = factory.Create(UseTurbo);

                controlledUpPressed = factory.Create(MoveUp);
                controlledDownPressed = factory.Create(MoveDown);

                SubscribeActions();
            }
        }

        public void Disable()
        {
            UnsubscribeActions();
        }

        private void DoRotation(RoutinePhase phase)
        {
            OnRotation.action?.Invoke(input.RotationAction.ReadValue<Vector2>(), phase);
        }

        private void DoAim(RoutinePhase phase)
        {
            OnAim.action?.Invoke(input.AimAction.ReadValue<Vector2>(), phase);
        }

        private void UsePrimeWeapon(RoutinePhase phase)
        {
            OnPrimeWeapon.action?.Invoke(phase);
        }

        private void UseSecondaryWeapon(RoutinePhase phase)
        {
            OnSecondaryWeapon.action?.Invoke(phase);
        }

        private void UseAdvanced(RoutinePhase phase)
        {
            OnAdvanced.action?.Invoke(phase);
        }

        private void UseTraps(RoutinePhase phase)
        {
            OnTraps.action?.Invoke(phase);
        }

        private void UseTurbo(RoutinePhase phase)
        {
            OnTurbo.action?.Invoke(phase);
        }

        private void UseBreaks(RoutinePhase phase)
        {
            OnBreaks.action?.Invoke(phase);
        }

        private void MoveUp(RoutinePhase phase)
        {
            OnUpPressed.action?.Invoke(phase);
        }

        private void MoveDown(RoutinePhase phase)
        {
            OnDownPressed.action?.Invoke(phase);
        }


        private void SubscribeActions()
        {
            input.OnRotationStart += controlledRotation.Start;
            input.OnRotationComplete += controlledRotation.Cancel;

            input.OnAimStart += controlledAim.Start;
            input.OnAimComplete += controlledAim.Cancel;

            input.OnSecondaryWeaponStart += controlledSecondaryWeapon.Start;
            input.OnSecondaryWeaponComplete += controlledSecondaryWeapon.Cancel;

            input.OnPrimeWeaponStart += controlledPrimeWeapon.Start;
            input.OnPrimeWeaponComplete += controlledPrimeWeapon.Cancel;

            input.OnAdvancedStart += controlledAdvanced.Start;
            input.OnAdvancedComplete += controlledAdvanced.Cancel;

            input.OnTrapsStart += controlledTraps.Start;
            input.OnTrapsComplete += controlledTraps.Cancel;

            input.OnTurboStart += controlledTurbo.Start;
            input.OnTurboComplete += controlledTurbo.Cancel;

            input.OnBreaksStart += controlledBreaks.Start;
            input.OnBreaksComplete += controlledBreaks.Cancel;
        }

        private void UnsubscribeActions()
        {
            input.OnRotationStart -= controlledRotation.Start;
            input.OnRotationComplete -= controlledRotation.Cancel;

            input.OnAimStart -= controlledAim.Start;
            input.OnAimComplete -= controlledAim.Cancel;

            input.OnSecondaryWeaponStart -= controlledSecondaryWeapon.Start;
            input.OnSecondaryWeaponComplete -= controlledSecondaryWeapon.Cancel;

            input.OnPrimeWeaponStart -= controlledPrimeWeapon.Start;
            input.OnPrimeWeaponComplete -= controlledPrimeWeapon.Cancel;

            input.OnAdvancedStart -= controlledAdvanced.Start;
            input.OnAdvancedComplete -= controlledAdvanced.Cancel;

            input.OnTrapsStart -= controlledTraps.Start;
            input.OnTrapsComplete -= controlledTraps.Cancel;

            input.OnTurboStart -= controlledTurbo.Start;
            input.OnTurboComplete -= controlledTurbo.Cancel;

            input.OnBreaksStart -= controlledBreaks.Start;
            input.OnBreaksComplete -= controlledBreaks.Cancel;
        }

        public class Factory : PlaceholderFactory<int, ControlledRoutine.Factory, Control>
        {

        }
    }


    public interface IControlFactory
    {
        IControl Get(IIdentifiers identifiers);
    }

    public class ControlFactory : IControlFactory
    {
        [Inject] private Control.Factory humanFactory;
        [Inject] private ControlledRoutine.Factory factory;
        public IControl Get(IIdentifiers identifiers)
        {
            switch (identifiers.PlayerType)
            {
                case PlayerType.human:
                    var control = humanFactory.Create(identifiers.ID, factory);
                    return control;

                case PlayerType.ai:
                    
                default:
                    throw new System.ArgumentException();
            }
        }
    }

    public class ControlEvent<T>
    {
        public Action<T> action;
    }

    public class ControlEvent<T1, T2>
    {
        public Action<T1, T2> action; 
    }
}

