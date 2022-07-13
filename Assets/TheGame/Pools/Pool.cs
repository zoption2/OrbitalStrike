using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TheGame
{
    public class Pool
    {
        private IPoolable referance;
        private Stack<IPoolable> pool = new Stack<IPoolable>();

        private GameObject poolHolder;
        [Inject] private IMonoInstantiator instantiator;

        public Pool(IPoolable referance)
        {
            this.referance = referance;
        }

        private static GameObject AllPools { get; } = new GameObject("Pools");

        private GameObject PoolHolder
        {
            get
            {
                if (poolHolder is null)
                {
                    poolHolder = new GameObject(referance.GetType() + "Pool");
                    poolHolder.transform.parent = AllPools.transform;
                }
                return poolHolder;
            }
        }

        public IPoolable Get(Vector3 position, Quaternion rotation)
        {
            IPoolable result;
            if (pool.Count > 0)
            {
                result = pool.Pop();
                result.transform.SetPositionAndRotation(position, rotation);
                result.gameObject.SetActive(true);
                result.OnRestore();
            }
            else
            {
                var go = instantiator.CreateObject(referance, position, rotation, PoolHolder.transform);
                result = go.GetComponent<IPoolable>();
                result.OnCreate();
            }
            return result;
        }

        public void Store(IPoolable poolable)
        {
            poolable.OnStore();
            poolable.gameObject.SetActive(false);
            pool.Push(poolable);
        }

        public class Factory : PlaceholderFactory<IPoolable, Pool>
        {

        }
    }
}

