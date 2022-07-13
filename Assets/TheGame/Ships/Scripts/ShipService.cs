using System.Threading.Tasks;

namespace TheGame
{
    public class ShipService
    {
        //public Task<Ship> GetShip(IIdentifiers identifiers, ShipType type)
        //{

        //}
    }

    public interface IShipService
    {
        Task<Ship> GetShip(IIdentifiers identifiers, ShipType type);
    }
}

