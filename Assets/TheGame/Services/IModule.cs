using UnityEngine;

namespace TheGame
{
    public interface IModule : IPoolable
    {
        bool IsAvailable();
        ModuleInfo Info { get; }
        void InitModule(IPlayer player);
        void LeaveModule(IPlayer identifiers);
    }

    [System.Serializable]
    public class ModuleInfo
    {
        [field: SerializeField] public Sprite Icon;
        [field: SerializeField] public string Title;
        [field: SerializeField] public string Description;
    }

    public class Extensions
    {
        public static void ChangeModule(IModule current, IModule changeTo, IPlayer player)
        {
            if (changeTo.IsAvailable())
            {
                current.LeaveModule(player);
                changeTo.InitModule(player);
            }
        }
    }
}

