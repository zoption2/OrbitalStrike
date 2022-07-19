using Zenject;

namespace TheGame
{
    public class PlayerFactory : IPlayerFactory
    {
        [Inject] private IMonoInstantiator monoInstantiator;
        [Inject] private PrefabReferanceHolder prefabHolder;
        [Inject] private IControlFactory controlFactory;

        public IPlayer Get(IIdentifiers identifiers, int splitPlayers, int currentPlayerNo)
        {
            var prefab = prefabHolder.Player;
            var go = monoInstantiator.CreateObject(prefab);
            var player = go.GetComponent<Player>();
            player.SetIdentifiers(identifiers);
            var control = controlFactory.Get(identifiers);
            player.SetControl(control);
            player.InitCameraController(null, splitPlayers, currentPlayerNo);
            return player;
        }
    }

    public interface IPlayerFactory
    {
        IPlayer Get(IIdentifiers identifiers, int splitPlayers, int currentPlayerNo);
    }
}

