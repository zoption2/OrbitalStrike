using UnityEngine.InputSystem;
using System;

namespace TheGame
{
    public class ControlActions
    {
        public PlayerInput Input { get; private set; }

        public event Action OnRotationStart;
        public event Action OnRotationComplete;

        public event Action OnAimStart;
        public event Action OnAimComplete;

        public event Action OnAdvancedStart;
        public event Action OnAdvancedComplete;

        public event Action OnPrimeWeaponStart;
        public event Action OnPrimeWeaponComplete;

        public event Action OnSecondaryWeaponStart;
        public event Action OnSecondaryWeaponComplete;

        public event Action OnTurboStart;
        public event Action OnTurboComplete;

        public event Action OnBreaksStart;
        public event Action OnBreaksComplete;

        public event Action OnTrapsStart;
        public event Action OnTrapsComplete;


        public InputAction PrimeWeaponAction { get; private set; }
        public InputAction SecondaryWeaponAction { get; private set; }
        public InputAction AdvancedAction { get; private set; }
        public InputAction RotationAction { get; private set; }
        public InputAction AimAction { get; private set; }
        public InputAction BreaksAction { get; private set; }
        public InputAction TurboAction { get; private set; }
        public InputAction TrapsAction { get; private set; }

        private const string rotation = "Rotation";
        private const string aim = "Aim";
        private const string advanced = "Advanced";
        private const string primeWeapon = "PrimeWeapon";
        private const string secondaryWeapon = "SecondaryWeapon";
        private const string turbo = "Turbo";
        private const string breaks = "Breaks";
        private const string traps = "Traps";

        public ControlActions(PlayerInput input)
        {
            Input = input;
        }

        public void SubscribeEvents()
        {
            RotationAction = Input.actions[rotation];
            RotationAction.started += ctx => OnRotationStart?.Invoke();
            RotationAction.canceled += ctx => OnRotationComplete?.Invoke();
            RotationAction.Enable();

            AimAction = Input.actions[aim];
            AimAction.started += ctx => OnAimStart?.Invoke();
            AimAction.canceled += ctx => OnAimComplete?.Invoke();
            AimAction.Enable();

            SecondaryWeaponAction = Input.actions[secondaryWeapon];
            SecondaryWeaponAction.started += ctx => OnSecondaryWeaponStart?.Invoke();
            SecondaryWeaponAction.canceled += ctx => OnSecondaryWeaponComplete?.Invoke();
            SecondaryWeaponAction.Enable();

            AdvancedAction = Input.actions[advanced];
            AdvancedAction.started += ctx => OnAdvancedStart?.Invoke();
            AdvancedAction.canceled += ctx => OnAdvancedComplete?.Invoke();
            AdvancedAction.Enable();

            PrimeWeaponAction = Input.actions[primeWeapon];
            PrimeWeaponAction.started += ctx => OnPrimeWeaponStart?.Invoke();
            PrimeWeaponAction.canceled += ctx => OnPrimeWeaponComplete?.Invoke();
            PrimeWeaponAction.Enable();

            TurboAction = Input.actions[turbo];
            TurboAction.started += ctx => OnTurboStart?.Invoke();
            TurboAction.canceled += ctx => OnTurboComplete?.Invoke();
            TurboAction.Enable();

            BreaksAction = Input.actions[breaks];
            BreaksAction.started += ctx => OnBreaksStart?.Invoke();
            BreaksAction.canceled += ctx => OnBreaksComplete?.Invoke();
            BreaksAction.Enable();

            TrapsAction = Input.actions[traps];
            TrapsAction.started += ctx => OnTrapsStart?.Invoke();
            TrapsAction.canceled += ctx => OnTrapsComplete?.Invoke();
            TrapsAction.Enable();
        }
    }
}

