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

    public interface IMothership : IModule
    {
        IModule CaptainsBridge { get; }
        IModule Lobby { get; }
    }

    public interface IProjectiles : IPoolable
    {
        void Initialize(IIdentifiers identifiers);
    }
}

