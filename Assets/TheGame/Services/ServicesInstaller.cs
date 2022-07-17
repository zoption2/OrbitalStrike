using Zenject;
using TheGame;
using UnityEngine;


public partial class ServicesInstaller : MonoInstaller
{
    [SerializeField] private MonoReferences reference;
    [SerializeField] private OnlevelHelper onlevelHelper;


    public override void InstallBindings()
    {
        BindMonoFactories();
        BindMonoServices();
        BindServices();
        BindPlayerControl();
    }

    private void BindPlayerControl()
    {
        Container.BindFactory<System.Action<RoutinePhase>, ControlledRoutine, ControlledRoutine.Factory>();
        Container.BindFactory<int, ControlledRoutine.Factory, Control, Control.Factory>();
        Container.Bind<IControlFactory>().To<ControlFactory>().AsSingle();
    }

    private void BindMonoServices()
    {
        Container.Bind<IMonoInstantiator>().To<MonoInstantiator>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<IGlobalFixedUpdateProvider>().To<GlobalFixedUpdateProvider>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<IGlobalUpdateProvider>().To<GlobalUpdateProvider>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<PrefabReferanceHolder>().FromInstance(reference.prefabHolder).AsSingle();

        Container.Bind<OnlevelHelper>().FromInstance(onlevelHelper).AsSingle();
    }

    private void BindServices()
    {
        Container.Bind<IPoolController>().To<PoolController>().AsSingle();
        Container.BindFactory<TheGame.IPoolable, Pool, Pool.Factory>();
        Container.Bind<IPlayersService>().To<PlayersService>().AsSingle();
        Container.Bind<IScreenFactory>().To<ScreenFactory>().AsSingle();
        Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
        Container.Bind<IGameSettings>().To<IngameSettings>().AsSingle();
    }

    private void BindMonoFactories()
    {
        Container.Bind<IRocketFactory>().FromInstance(reference.RocketFactory).AsSingle();
        Container.Bind<IMachineGunFactory>().FromInstance(reference.MachineGunFactory).AsSingle();
        Container.Bind<IShipFactory>().FromInstance(reference.ShipFactory).AsSingle();
    }
}
