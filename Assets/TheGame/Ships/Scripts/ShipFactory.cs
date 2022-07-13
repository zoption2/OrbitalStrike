namespace TheGame
{
    public class ShipFactory : AddressablesFactory<Ship, ShipType>, IShipFactory
    {

    }

    public interface IShipFactory : IAddressableFactory<Ship, ShipType>
    {

    }

}

