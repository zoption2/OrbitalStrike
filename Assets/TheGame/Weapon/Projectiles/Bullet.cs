using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGame
{
    public class Bullet : Projectile
    {
        [SerializeField] private TrailRenderer trailRenderer;
        public override Enum WeaponType => MachineGunType.simpleGun;

        public override void OnRestore(IIdentifiers identifiers)
        {
            base.OnRestore(identifiers);
            trailRenderer.Clear();
            trailRenderer.enabled = true;
        }

        public override void OnStore()
        {
            base.OnStore();
            trailRenderer.enabled = false;
        }
    }
}

