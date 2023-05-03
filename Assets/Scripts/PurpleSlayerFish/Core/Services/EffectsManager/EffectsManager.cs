using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Core.Services.EffectsManager
{
    public class EffectsManager
    {
        private const string EXPLOSION_PREFAB = "Explosion";
        [Inject] private IPoolProvider<EffectsPoolData> _poolProvider;

        private Transform _tempTransform;

        public void CastExplosion(Vector2 position)
        {
            _tempTransform = _poolProvider.Get(EXPLOSION_PREFAB).transform;
            _tempTransform.position = new Vector3(position.x, position.y, _tempTransform.position.z);
        }
    }
}