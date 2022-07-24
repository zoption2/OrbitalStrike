using UnityEngine;

namespace TheGame
{
    public interface IModule : IPoolable, IControlled
    {
        bool IsAvailable();
        ModuleInfo Info { get; }
        CameraPrefs CameraPrefs { get; }
        void JoinModule(IPlayer player);
        void LeaveModule(IPlayer player);
    }

    [System.Serializable]
    public class ModuleInfo
    {
        [field: SerializeField] public Sprite Icon;
        [field: SerializeField] public string Title;
        [field: SerializeField] public string Description;
    }

    [System.Serializable]
    public class CameraPrefs
    {
        [field: SerializeField] public float OrthoSize = 30f;
    }
}

