using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Services.Pools.Config;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace PurpleSlayerFish.Core.Services.Pools.PoolProvider
{
    public interface IPoolProvider<T> where T : PoolData
    {
        void Initialized();
        AbstractBehaviour Get(string poolKey);
        T0 Get<T0>() where T0 : AbstractBehaviour => Get(typeof(T0).Name) as T0;
        void Release(string poolKey, AbstractBehaviour view);
        void Release<T0>(T0 view) where T0 : AbstractBehaviour => Release(typeof(T0).Name, view);
    }
    
    public class TransformPool
    {
        public Transform Transform { get; }
        public IObjectPool<AbstractBehaviour> ObjectPool { get; }

        public TransformPool(Transform transform, IObjectPool<AbstractBehaviour> objectPool)
        {
            Transform = transform;
            ObjectPool = objectPool;
        }
    }
}