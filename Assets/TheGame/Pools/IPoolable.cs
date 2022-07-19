using UnityEngine;
using System.Collections.Generic;

namespace TheGame
{
    public interface IPoolable
    {
        GameObject gameObject { get; }
        Transform transform { get; }
        void OnCreate();
        void OnRestore();
        void OnStore();
    }

    public interface IMothership : IPoolable
    {
        IModule CaptainsBridge { get; }
        IModule Lobby { get; }
    }

    public interface IMothershipUI : IPoolable
    {
        void Initialize(IPlayer player, List<IModule> mothershipModules);
    }

    public interface IProjectiles : IPoolable
    {
        void Initialize(IIdentifiers identifiers);
    }
}

