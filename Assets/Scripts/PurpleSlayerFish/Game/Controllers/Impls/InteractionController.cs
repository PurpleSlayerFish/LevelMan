using System.Collections.Generic;
using PurpleSlayerFish.Core.Services;
using PurpleSlayerFish.Core.Services.Pools.Config;
using PurpleSlayerFish.Core.Services.Pools.PoolProvider;
using PurpleSlayerFish.Core.Services.ScriptableObjects.GameConfig;
using PurpleSlayerFish.Game.Processors.InteractionProcessor;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace PurpleSlayerFish.Game.Controllers
{
    public class InteractionController
    {
        [Inject] private GameConfig _gameConfig;
        [Inject] private IPoolProvider<BehaviourPoolData> _poolProvider;

        public List<AInteraction> Interactions = new();
        private VectorUtils _vectorUtils;
        
        private PotInteraction _pot;

        public AInteraction CheckIntersections(Vector3 position,float distance)
        {
            for (int i = 0; i < Interactions.Count; i++)
                if (_vectorUtils.InDistance(Interactions[i].transform.position - position, distance))
                    return Interactions[i];
            return null;
        }

        private float _navMeshOffset = NavMesh.GetSettingsByID(0).voxelSize;

        public void Spawn(Vector3 position, string prefabName)
        {
            if (prefabName == "PotBehaviour")
            {
                SetPot(position);
                return;
            }
            
            var behaviour = _poolProvider.Get(prefabName) as InteractionBehavior;
            Interactions.Add(behaviour.Interaction);
            behaviour.transform.position = new Vector3(position.x, -_navMeshOffset, position.z);
        }
        
        public void Kill(InteractionBehavior interaction, string prefabName)
        {
            Interactions.Remove(interaction.Interaction);
            _poolProvider.Release(prefabName, interaction);
        }

        public void AddPot(PotInteraction pot)
        {
            _pot = pot;
            Interactions.Add(pot);
        }

        public void SetPot(Vector3 position)
        {
            _pot.gameObject.SetActive(true);
            _pot.transform.position = new Vector3(position.x, -_navMeshOffset, position.z);
        }
        
        public void HidePot()
        {
            _pot.gameObject.SetActive(false);
        }
    }
}