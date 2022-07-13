using UnityEngine;

namespace TheGame
{
    public class JetFighterShip : Ship
    {
        
        [SerializeField] private MachineGunController gunController;
        [SerializeField] private RocketController rocketController;

        public override ShipType ShipType => ShipType.jetFighter;


        protected override void Init()
        {
            rotation.Init(control.OnRotation);
            gunController.Init(identifiers, MachineGunType.simpleGun, control.OnPrimeWeapon);
            rocketController.Init(identifiers, RocketType.autoAimAntiShip, control.OnSecondaryWeapon);
            health.Init(identifiers);
        }
    }
}

