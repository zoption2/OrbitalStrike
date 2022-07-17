using Zenject;

namespace TheGame
{
    public class PlayerFactory : IPlayerFactory
    {
        [Inject] private IMonoInstantiator monoInstantiator;
        [Inject] private PrefabReferanceHolder prefabHolder;

        public Player Get(IIdentifiers identifiers)
        {
            var prefab = prefabHolder.Player;
            var go = monoInstantiator.CreateObject(prefab);
            var player = go.GetComponent<Player>();
            player.Init(identifiers);
            return player;
        }
    }

    public interface IPlayerFactory
    {
        Player Get(IIdentifiers identifiers);
    }
}

