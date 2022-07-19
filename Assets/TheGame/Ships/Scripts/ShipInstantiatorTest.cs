using UnityEngine;
using Zenject;

namespace TheGame
{
    public class ShipInstantiatorTest : MonoBehaviour
    {
        [Inject] private IGameSettings setti;
        [Inject] private IShipFactory shipFactory;
        [Inject] private IPoolController pool;
        [Inject] private IPlayersService playersService;
        [Inject] private IControlFactory controlFactory;

        private async void Start()
        {
            setti.TotalPlayers = 2;
            playersService.Initialize(setti.TotalPlayers, 1);

            //var waiter = shipFactory.Preload(ShipType.jetFighter);
            //var waiter = shipFactory.Preload(ShipType.mothership);
            var waiter = shipFactory.GetPrefab(ShipType.mothership);
            await waiter;
            var newShip = waiter.Result;
            //var newShip = shipFactory.TryGetPrefab(ShipType.mothership, null);
            IMothership mothership = (IMothership)pool.Get(ShipType.mothership, Vector2.zero, Quaternion.identity, newShip);

            for (int i = 0; i < setti.TotalPlayers; i++)
            {

                var player = playersService.AddPlayer(0, out IIdentifiers identifiers);
                mothership.Lobby.InitModule(player);
                player.CameraControl.ChangeCameraTarget(mothership.transform);
            }
        }
    }
}

