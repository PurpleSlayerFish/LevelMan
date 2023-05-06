using PurpleSlayerFish.Core.Services;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Core.Behaviours
{
    public class AudioBehaviour : AbstractBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [Inject] private MathUtils _mathUtils;
        public Temporator Temporator { get; set; }

        public int ClipDuration => _audioSource.loop ? -1 : Mathf.FloorToInt(_audioSource.clip.length * 1000);
        
        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
