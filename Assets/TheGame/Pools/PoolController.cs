using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

namespace TheGame
{
    public interface IPoolController
    {
        IPoolable Get(Enum name, Vector2 position, Quaternion rotation, IPoolable prefab);
        void Release(Enum args, IPoolable poolable);
    }

    public class PoolController : IPoolController
    {
        private readonly Dictionary<Enum, Pool> pools = new Dictionary<Enum, Pool>();

        [Inject] private Pool.Factory poolFactory;

        public IPoolable Get(Enum name, Vector2 position, Quaternion rotation, IPoolable prefab)
        {
            if (!pools.ContainsKey(name))
            {
                var pool = poolFactory.Create(prefab);
                pools.Add(name, pool);
            }

            var poolable = pools[name].Get(position, rotation);
            return poolable;
        }

        public void Release(Enum args, IPoolable poolable)
        {
            if (pools.ContainsKey(args))
            {
                pools[args].Store(poolable);
            }
        }
    }

    public interface ISettings
    {
        Enum Name { get; }
        IIdentifiers Identifiers { get; }
        Vector2 Position { get; }
        Quaternion Rotation { get; }
        IPoolable Referance { get; }
    }

    public class PoolableSettings : ISettings
    {
        public Enum Name { get; }
        public IIdentifiers Identifiers { get; }
        public Vector2 Position { get; }
        public Quaternion Rotation { get; }
        public IPoolable Referance { get; }


        public PoolableSettings(
            Enum name,
            IIdentifiers identifiers,
            Vector2 position,
            Quaternion rotation,
            IPoolable referance)
        {
            Name = name;
            Position = position;
            Rotation = rotation;
            Identifiers = identifiers;
            Referance = referance;
        }
    }

}

