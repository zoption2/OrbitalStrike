using UnityEngine;
using System;

namespace TheGame
{
    public interface IPoolable
    {
        GameObject gameObject { get; }
        Transform transform { get; }
        void OnCreate(IIdentifiers identifiers);
        void OnRestore(IIdentifiers identifiers);
        void OnStore();
    }
}

