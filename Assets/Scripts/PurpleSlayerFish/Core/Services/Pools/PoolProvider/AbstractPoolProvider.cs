using System;
using System.Collections.Generic;
using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Global;
using PurpleSlayerFish.Core.Services.AssetProvider;
using PurpleSlayerFish.Core.Services.Pools.Config;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Object = UnityEngine.Object;

namespace PurpleSlayerFish.Core.Services.Pools.PoolProvider
{
    public abstract class AbstractPoolProvider<T> : IPoolProvider<T>, IDisposable where T : PoolData
    {
        protected abstract string PoolerConfigName { get; }
        protected abstract string RootName { get; }

        [Inject] protected IAssetProvider _assetProvider;
        
        protected Dictionary<string, TransformPool> _pools;
        protected IPoolConfig<T> PoolConfig;
        protected Transform _rootTransform;
        protected Transform _tempTransform;

        public void Initialized()
        {
            _pools = new Dictionary<string, TransformPool>();
            PoolConfig = _assetProvider.Get<AbstractPoolConfig<T>>(GameGlobal.SCRIPTABLE_OBJECTS_BUNDLE, PoolerConfigName);
            _rootTransform = InstantiateTransform();
            _rootTransform.name = RootName;
            
            T data;
            for (int i = 0; i < PoolConfig.GetData.Length; i++)
            {
                data = PoolConfig.GetData[i];
                _pools.Add(data.PrefabName, CreateTransformPool(data));
            }
        }

        private TransformPool CreateTransformPool(in T data)
        {
            _tempTransform = InstantiateTransform();
            _tempTransform.name = data.PrefabName;
            _tempTransform.SetParent(_rootTransform);
            var cachedData = data;
            var cachedParent = _tempTransform;
            return new TransformPool(_tempTransform,
                new ObjectPool<AbstractBehaviour>(
                    () => CreatePooledItem(cachedParent, cachedData),
                    OnTakeFromPool,
                    view => OnReturnedToPool(view),
                    OnDestroyPoolObject,
                    false,
                    data.DefaultCapacity,
                    data.MaxPoolSize));
        }

        protected abstract AbstractBehaviour CreatePooledItem(Transform parent, T data);
        protected virtual void OnTakeFromPool(AbstractBehaviour view) => view.gameObject.SetActive(true);
        private void OnReturnedToPool(AbstractBehaviour view)
        {
            if (!Application.isPlaying)
                return;
            view.gameObject.SetActive(false);
        }

        private void OnDestroyPoolObject(AbstractBehaviour view)
        {
            if (view != null)
                Object.Destroy(view.gameObject);
        }

        private Transform InstantiateTransform() =>
            Object.Instantiate(_assetProvider.Get<GameObject>(GameGlobal.CORE_BUNDLE, GameGlobal.TRANSFORM_PREFAB)).transform;

        public AbstractBehaviour Get(string poolKey)
        {
            if (!_pools.ContainsKey(poolKey))
                throw new KeyNotFoundException("Prefab '" + poolKey + "' not presented in PoolProvider with data'" + typeof(T).Name + "'.");
            return _pools[poolKey].ObjectPool.Get();
        }

        public void Release(string poolKey, AbstractBehaviour view) => _pools[poolKey].ObjectPool.Release(view);

        public void Dispose()
        {
            var enumerator = _pools.GetEnumerator();
            while (enumerator.MoveNext())
                enumerator.Current.Value.ObjectPool.Clear();
        }
    }
}