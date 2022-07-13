namespace TheGame
{
    public class RocketFactory : AddressablesFactory<RocketInfo, RocketType>, IRocketFactory
    {

    }

    public interface IRocketFactory : IAddressableFactory<RocketInfo, RocketType>
    {

    }
}

