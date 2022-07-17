using UnityEngine;

namespace TheGame
{
    public interface IModule : IPoolable
    {
        bool IsAvailable();
        ModuleInfo Info { get; }
        void InitModule(Player player);
        void LeaveModule(Player identifiers);
    }

    [System.Serializable]
    public class ModuleInfo
    {
        [field: SerializeField] public Sprite Icon;
        [field: SerializeField] public string Title;
        [field: SerializeField] public string Description;
    }
}

