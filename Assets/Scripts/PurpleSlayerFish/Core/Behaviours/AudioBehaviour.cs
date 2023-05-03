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

        public int ClipDuation => Mathf.FloorToInt(_audioSource.clip.length * 1000);
    }
}
