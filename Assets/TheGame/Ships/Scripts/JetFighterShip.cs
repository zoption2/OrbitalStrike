using UnityEngine;

namespace TheGame
{
    public class JetFighterShip : Ship
    {
        
        [SerializeField] private MachineGunController gunController;
        [SerializeField] private RocketController rocketController;

        public override ShipType ShipType => ShipType.jetFighter;

        public override void JoinModule(IPlayer player)
        {
            base.JoinModule(player);
            rotation.Init(control.OnLeftStickUse);
            gunController.Init(identifiers, MachineGunType.simpleGun, control.OnRightTriggerUse);
            rocketController.Init(identifiers, RocketType.autoAimAntiShip, control.OnAButton);
            health.Init(identifiers);
        }
    }
}

