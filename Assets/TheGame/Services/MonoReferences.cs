using UnityEngine;


namespace TheGame
{
    [CreateAssetMenu(fileName = "MonoReferences", menuName = "ScriptableObjects/Mono References")]
    public class MonoReferences : ScriptableObject
    {
        [field: SerializeField] public ShipFactory ShipFactory { get; private set; }
        [field: SerializeField] public MothershipUIFactory MothershipUIFactory { get; private set; }
        [field: SerializeField] public CameraController PlayerCamera { get; private set; }
        [field: SerializeField] public MachineGunFactory MachineGunFactory { get; private set; }
        [field: SerializeField] public RocketFactory RocketFactory { get; private set; }
        [field: SerializeField] public PrefabReferanceHolder prefabHolder { get; private set; }
    }
}
