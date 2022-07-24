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

        private ControlledRoutine controlledDPad;

        private ControlledRoutine.Factory factory;

        private const string SHIP_MAP = "ShipControl";
        private const string UI_MAP = "UIControl";

        public ControlEvent<Vector2, RoutinePhase> OnLeftStickUse { get; } = new ControlEvent<Vector2, RoutinePhase>();
        public ControlEvent<Vector2, RoutinePhase> OnRightStickUse { get; } = new ControlEvent<Vector2, RoutinePhase>();
        public ControlEvent<RoutinePhase> OnRightTriggerUse { get; } = new ControlEvent<RoutinePhase>();
        public ControlEvent<RoutinePhase> OnAButton { get; } = new ControlEvent<RoutinePhase>();
        public ControlEvent<RoutinePhase> OnYButton { get; } = new ControlEvent<RoutinePhase>();
        public ControlEvent<RoutinePhase> OnBButton { get; } = new ControlEvent<RoutinePhase>();
        public ControlEvent<RoutinePhase> OnXButton { get; } = new ControlEvent<RoutinePhase>();
        public ControlEvent<RoutinePhase> OnRightShoulder { get; } = new ControlEvent<RoutinePhase>();

        public ControlEvent<Vector2, RoutinePhase> OnMoveDPad { get; } = new ControlEvent<Vector2, RoutinePhase>();



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

                controlledDPad = factory.Create(MoveUp);

                SubscribeActions();
            }
        }

        public void ChangeActionMap(ActionMapType actionMap)
        {
            switch (actionMap)
            {
                case ActionMapType.ship:
                    playerInput.defaultActionMap = SHIP_MAP;
                    break;
                case ActionMapType.ui:
                    playerInput.defaultActionMap = UI_MAP;
                    break;
            }
        }

        public void Disable()
        {
            UnsubscribeActions();
        }

        private void DoRotation(RoutinePhase phase)
        {
            OnLeftStickUse.action?.Invoke(input.RotationAction.ReadValue<Vector2>(), phase);
        }

        private void DoAim(RoutinePhase phase)
        {
            OnRightStickUse.action?.Invoke(input.AimAction.ReadValue<Vector2>(), phase);
        }

        private void UsePrimeWeapon(RoutinePhase phase)
        {
            OnRightTriggerUse.action?.Invoke(phase);
        }

        private void UseSecondaryWeapon(RoutinePhase phase)
        {
            OnAButton.action?.Invoke(phase);
        }

        private void UseAdvanced(RoutinePhase phase)
        {
            OnYButton.action?.Invoke(phase);
        }

        private void UseTraps(RoutinePhase phase)
        {
            OnRightShoulder.action?.Invoke(phase);
        }

        private void UseTurbo(RoutinePhase phase)
        {
            OnBButton.action?.Invoke(phase);
        }

        private void UseBreaks(RoutinePhase phase)
        {
            OnXButton.action?.Invoke(phase);
        }

        private void MoveUp(RoutinePhase phase)
        {
            OnMoveDPad.action?.Invoke(input.MoveDPad.ReadValue<Vector2>(), phase);
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

            input.OnMoveDPadStart += controlledDPad.Start;
            input.OnMoveDPadComplete += controlledDPad.Cancel;
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

            input.OnMoveDPadStart -= controlledDPad.Start;
            input.OnMoveDPadComplete -= controlledDPad.Cancel;
        }

        public class Factory : PlaceholderFactory<int, ControlledRoutine.Factory, Control>
        {

        }
    }

    public enum ActionMapType
    {
        ship,
        ui
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

