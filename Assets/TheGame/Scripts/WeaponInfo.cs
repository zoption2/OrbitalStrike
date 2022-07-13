using UnityEngine;
using System;

namespace TheGame
{
    public class WeaponInfo<T> : MonoBehaviour where T : Enum
    {
        [field: SerializeField] public T WeaponType;
        [field: SerializeField] public int MaxAmmo { get; private set; }
        [field: SerializeField] public int ShotDelayMS { get; private set; }
        [field: SerializeField] public int ReloadMS { get; private set; }
        [field: SerializeField] public Projectile projectilePrefab { get; private set; }
    }


    public enum WeaponType
    {
        gun,
        rocket
    }

    public enum MachineGunType
    {
        simpleGun,

    }

    public enum RocketType
    {
        autoAimAntiShip
    }
}

