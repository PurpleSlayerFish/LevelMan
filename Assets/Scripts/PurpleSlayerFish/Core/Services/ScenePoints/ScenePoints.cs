using SerializableDictionary.Scripts;
using UnityEngine;

namespace PurpleSlayerFish.Core.Services.ScenePoints
{
    public class ScenePoints : MonoBehaviour
    {
        [SerializeField] private SerializableDictionary<string, Transform> _point;
        public SerializableDictionary<string, Transform> Points => _point;
    }
}