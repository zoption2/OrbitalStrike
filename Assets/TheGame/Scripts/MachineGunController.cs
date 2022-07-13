using UnityEngine;
using Zenject;

namespace TheGame
{
    public class MachineGunController : MonoBehaviour
    {
        [SerializeField] protected Transform[] aimPositions;

        private IPoolController pool;
        private IControl control;
        private ControlEvent<RoutinePhase> controlAction;
        private MachineGunInfo info;
        private IIdentifiers identifiers;
        private int ammo;
        private bool canShot;
        private IMachineGunFactory weaponFactory;

        [Inject]
        private void Constructor(IMachineGunFactory weaponFactory, IPoolController poolController)
        {
            this.weaponFactory = weaponFactory;
            pool = poolController;
        }

        public void Init(IIdentifiers identifiers, MachineGunType weaponType, ControlEvent<RoutinePhase> contollerCallback)
        {
            this.identifiers = identifiers;
            controlAction = contollerCallback;
            weaponFactory.TryGetPrefab(weaponType, OnSuccess, OnFail);

            void OnSuccess(MachineGunInfo args)
            {
                info = args;
                ammo = info.MaxAmmo;
                controlAction.action += Fire;
                canShot = true;
            }

            void OnFail()
            {

            }
        }

        private void Fire(RoutinePhase phase)
        {
            if (canShot)
            {
                for (int i = 0, j = aimPositions.Length; i < j; i++)
                {
                    if (ammo > 0)
                    {
                        var settings = new PoolableSettings(info.WeaponType
                                                            , identifiers
                                                            , aimPositions[i].position
                                                            , aimPositions[i].rotation
                                                            , info.projectilePrefab);
                        pool.Get(settings);
                        ammo--;
                    }
                    else break;
                }
                DelayShot();
            }
        }

        private async void DelayShot()
        {
            canShot = false;
            await System.Threading.Tasks.Task.Delay(info.ShotDelayMS);
            canShot = true;
        }
    }
}

