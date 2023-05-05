using Cinemachine;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Core.Behaviours
{
    public class FloatingCanvasBehaviour : AbstractBehaviour
    {
        [Inject] private CinemachineVirtualCamera _virtualCamera;
        [Inject] private Camera _camera;

        [SerializeField] private Canvas _canvas;


        private void Start() => _canvas.worldCamera = _camera;

        private void Update()
        {
            if (_virtualCamera != null)
            {
                transform.LookAt(transform.position + _virtualCamera.transform.forward);
            }
        }

        public override void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}