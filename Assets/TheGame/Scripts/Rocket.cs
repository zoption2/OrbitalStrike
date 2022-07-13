using UnityEngine;
using TheGame.AI;
using System;

namespace TheGame
{
    public class Rocket : Projectile
    {
        [SerializeField] private TargetDetector detector;
        public override Enum WeaponType => RocketType.autoAimAntiShip;

        public override void Initialize(IIdentifiers identifiers)
        {
            base.Initialize(identifiers);
            detector.Init(identifiers, fixedUpdateProvider);
        }

        public override void OnStore()
        {
            base.OnStore();
            detector.Release();
        }

        protected override void Move()
        {
            rigidBody.AddForce(transform.up * speed);
        }
    }
}

