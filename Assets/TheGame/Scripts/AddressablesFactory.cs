using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace TheGame
{
    public class AddressablesFactory<T1, T2> : MonoBehaviour, IAddressableFactory<T1, T2> where T1 : UnityEngine.Object where T2 : Enum
    {
        [SerializeField] private Data<T2>[] data;
        private Dictionary<T2, T1> loaded = new Dictionary<T2, T1>();

        public T1 TryGetPrefab(T2 type, Action<T1> succes, Action failback = null)
        {
            if (loaded.ContainsKey(type))
            {
                return loaded[type];
            }
            else
            {
                Load(type, succes, failback);
                return null;
            }
        }

        public async Task<T1> GetPrefab(T2 type)
        {
            if (loaded.ContainsKey(type))
            {
                return loaded[type];
            }
            for (int i = 0, j = data.Length; i < j; i++)
            {
                if (data[i].Type.Equals(type))
                {
                    AsyncOperationHandle<GameObject> opHandle = Addressables.LoadAssetAsync<GameObject>(data[i].referance);
                    await opHandle.Task;
                    if (opHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        if (opHandle.Result.TryGetComponent(out T1 product))
                        {
                            if (!loaded.ContainsKey(type))
                            {
                                loaded.Add(type, product);
                            }
                            return loaded[type];
                        }
                        else
                        {
                            throw new MissingComponentException();
                        }
                    }
                }
            }

            throw new System.ArgumentNullException();
        }

        private async void Load(T2 type, Action<T1> callback, Action failback)
        {
            if (loaded.ContainsKey(type))
            {
                var item = loaded[type];
                callback?.Invoke(item);
                return;
            }

            for (int i = 0, j = data.Length; i < j; i++)
            {
                if (data[i].Type.Equals(type))
                {
                    AsyncOperationHandle<GameObject> opHandle = Addressables.LoadAssetAsync<GameObject>(data[i].referance);
                    await opHandle.Task;
                    if (opHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        if (opHandle.Result.TryGetComponent(out T1 product))
                        {
                            if (!loaded.ContainsKey(type))
                            {
                                loaded.Add(type, product);
                            }
                            var item = loaded[type];
                            callback?.Invoke(item);
                        }
                        else
                        {
                            throw new MissingComponentException();
                        }
                    }
                    else
                    {
                        failback?.Invoke();
                        throw new NullReferenceException();
                    }
                }
            }
        }

        public async Task Preload(T2 type)
        {
            if (loaded.ContainsKey(type))
            {
                return;
            }

            for (int i = 0, j = data.Length; i < j; i++)
            {
                if (data[i].Type.Equals(type))
                {
                    if (!loaded.ContainsKey(type))
                    {
                        AsyncOperationHandle<GameObject> opHandle = Addressables.LoadAssetAsync<GameObject>(data[i].referance);
                        await opHandle.Task;
                        if (opHandle.Status == AsyncOperationStatus.Succeeded)
                        {
                            if (opHandle.Result.TryGetComponent(out T1 ship) && !loaded.ContainsKey(type))
                            {
                                loaded.Add(type, ship);
                            }
                        }
                    }
                }
            }
        }


        [System.Serializable]
        public class Data<T3> where T3 : T2
        {
            [field: SerializeField] public T3 Type;
            [field: SerializeField] public AssetReferenceGameObject referance { get; private set; }
        }
    }

    public interface IAddressableFactory<T1, T2> where T1 : UnityEngine.Object where T2 : Enum
    {
        T1 TryGetPrefab(T2 type, Action<T1> succes, Action failback = null);
        Task Preload(T2 type);
        Task<T1> GetPrefab(T2 type);
    }
}

