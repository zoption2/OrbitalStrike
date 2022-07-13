using UnityEngine;
using TheGame.AI;
using System;

namespace TheGame
{
    public class Rocket : Projectile
    {
        [SerializeField] private TargetDetector detector;
        public override Enum WeaponType => RocketType.autoAimAntiShip;

        public override void OnCreate(IIdentifiers identifiers)
        {
            base.OnCreate(identifiers);
            detector.Init(identifiers, fixedUpdateProvider);
        }

        public override void OnRestore(IIdentifiers identifiers)
        {
            base.OnRestore(identifiers);
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

