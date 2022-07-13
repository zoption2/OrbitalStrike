using UnityEngine;
using System;

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

    public interface IShip : IPoolable
    {
        void Initialize(IIdentifiers identifiers, IControl control);
    }

    public interface IProjectiles : IPoolable
    {
        void Initialize(IIdentifiers identifiers);
    }
}

