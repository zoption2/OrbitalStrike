using UnityEngine;

namespace TheGame
{
    public class JetFighterShip : Ship
    {
        
        [SerializeField] private MachineGunController gunController;
        [SerializeField] private RocketController rocketController;

        public override ShipType ShipType => ShipType.jetFighter;

        public override void InitModule(IPlayer player)
        {
            base.InitModule(player);
            rotation.Init(control.OnRotation);
            gunController.Init(identifiers, MachineGunType.simpleGun, control.OnPrimeWeapon);
            rocketController.Init(identifiers, RocketType.autoAimAntiShip, control.OnSecondaryWeapon);
            health.Init(identifiers);
        }
    }
}

