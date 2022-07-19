namespace TheGame
{
    public class MothershipUIFactory : AddressablesFactory<MothershipUI, UIType>, IMothershipUIFactory
    {

    }

    public interface IMothershipUIFactory : IAddressableFactory<MothershipUI, UIType>
    {

    }
}

