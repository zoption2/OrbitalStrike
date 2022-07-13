using UnityEngine;
using Zenject;

namespace TheGame
{
    public class ShipInstantiatorTest : MonoBehaviour
    {
        [SerializeField] private IngameSettings setti;
        [Inject] private IShipFactory shipFactory;
        [Inject] private IPoolController pool;
        [Inject] private IPlayersService playersService;
        [Inject] private IControlFactory controlFactory;
        private CameraController[] cameraController = new CameraController[2];

        [Inject]
        private void Constructor(CameraController controller1, CameraController controller2)
        {
            cameraController[0] = controller1;
            cameraController[1] = controller2;
        }

        private async void Start()
        {
            var waiter = shipFactory.Preload(ShipType.jetFighter);
            await waiter;

            for (int i = 0; i < setti.Players; i++)
            {
                var newShip = shipFactory.TryGetPrefab(ShipType.jetFighter, OnSuccess, OnFail);
                playersService.AddPlayer(i, out IIdentifiers identifiers);
                IShip poolable = (IShip)pool.Get(ShipType.jetFighter, Vector2.zero, Quaternion.identity, newShip);
                var control = controlFactory.Get(identifiers);
                poolable.Initialize(identifiers, control);
                cameraController[i].Init(poolable.transform, setti.Players, i + 1);

                void OnSuccess(Ship ship)
                {

                }

                void OnFail()
                {
                    Debug.LogError("Vse poshlo po pizde, bratuha");
                }
            }
        }
    }
}

