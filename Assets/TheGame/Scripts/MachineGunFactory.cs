namespace TheGame
{
    public class MachineGunFactory : AddressablesFactory<MachineGunInfo, MachineGunType>, IMachineGunFactory
    {

    }

    public interface IMachineGunFactory : IAddressableFactory<MachineGunInfo, MachineGunType>
    {

    }
}

