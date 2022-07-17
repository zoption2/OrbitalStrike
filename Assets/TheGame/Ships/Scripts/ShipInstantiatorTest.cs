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
            var waiter = shipFactory.Preload(ShipType.mothership);
            await waiter;

            var newShip = shipFactory.TryGetPrefab(ShipType.mothership, null);
            IMothership mothership = (IMothership)pool.Get(ShipType.mothership, Vector2.zero, Quaternion.identity, newShip);

            for (int i = 0; i < setti.TotalPlayers; i++)
            {

                playersService.AddPlayer(i, out IIdentifiers identifiers);
                var service = playersService.GetData(identifiers.ID);

                mothership.Lobby.InitModule(service);
                service.cameraController.SwitchTarget(mothership.transform);

                //void OnSuccess(Ship ship)
                //{

                //}

                //void OnFail()
                //{
                //    Debug.LogError("Vse poshlo po pizde, bratuha");
                //}
            }
        }
    }
}

