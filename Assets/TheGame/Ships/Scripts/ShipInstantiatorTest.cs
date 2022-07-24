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
            var waiter = shipFactory.GetPrefab(ShipType.mothership);
            await waiter;
            var newShip = waiter.Result;

            IMothership mothership = (IMothership)pool.Get(ShipType.mothership, Vector2.zero, Quaternion.identity, newShip);

            for (int i = 0; i < setti.TotalPlayers; i++)
            {

                var player = playersService.AddPlayer(0, out IIdentifiers identifiers);
                //mothership.JoinModule(player);
                player.Module.ChangeModule(mothership, player);
                //player.CameraControl.ChangeCameraTarget(mothership.transform);
            }
        }
    }
}

