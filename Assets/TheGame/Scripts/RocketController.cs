using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

namespace TheGame
{
    public class RocketController : MonoBehaviour
    {
        [SerializeField] protected Barrel[] aimPositions;

        private IPoolController pool;
        private IControl control;
        private ControlEvent<RoutinePhase> controlAction;
        private RocketInfo info;
        private IIdentifiers identifiers;
        private int ammo;
        private bool canShot;
        private IRocketFactory weaponFactory;

        [Inject]
        private void Constructor(IRocketFactory weaponFactory, IPoolController poolController)
        {
            this.weaponFactory = weaponFactory;
            pool = poolController;
        }

        public void Init(IIdentifiers identifiers, RocketType weaponType, ControlEvent<RoutinePhase> contollerCallback)
        {
            this.identifiers = identifiers;
            controlAction = contollerCallback;
            weaponFactory.TryGetPrefab(weaponType, OnSuccess, OnFail);

            void OnSuccess(RocketInfo args)
            {
                info = args;
                ammo = info.MaxAmmo;
                controlAction.action += Fire;
                FillBarrels();
                canShot = true;
            }

            void OnFail()
            {

            }
        }

        private void FillBarrels()
        {
            for (int i = 0, j = aimPositions.Length; i < j; i++)
            {
                aimPositions[i].Init(info.projectilePrefab.SpriteRenderer.sprite, info.ReloadMS);
                aimPositions[i].OnProjectileAwait += LoadBarrel;
                ammo--;
            }
        }

        private void Fire(RoutinePhase phase)
        {
            for (int i = 0, j = aimPositions.Length; i < j; i++)
            {
                if (canShot)
                {
                    if (aimPositions[i].IsReady)
                    {
                        aimPositions[i].Launch(identifiers, info.WeaponType, aimPositions[i].Aim.position, aimPositions[i].Aim.rotation, info.projectilePrefab);
                        canShot = false;
                        break;
                    }
                }
            }

            if (phase == RoutinePhase.complete)
            {
                canShot = true;
            }
        }

        private void LoadBarrel(Barrel barrel)
        {
            if (ammo <= 0)
            {
                barrel.SetEmpty();
                return;
            }

            barrel.LoadProjectile();
            ammo--;
        }
    }
}

