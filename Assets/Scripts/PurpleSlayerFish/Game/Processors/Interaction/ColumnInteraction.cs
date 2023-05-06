using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Global;
using PurpleSlayerFish.Core.Services.AssetProvider;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Core.Services.ScriptableObjects;
using PurpleSlayerFish.Core.Ui.Container;
using PurpleSlayerFish.Core.Ui.Windows.PauseWindow;
using PurpleSlayerFish.Game.Behaviours;
using PurpleSlayerFish.Game.Controllers.Impls;
using PurpleSlayerFish.Game.Processors.Interaction.Interactors;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.Interaction
{
    public class ColumnInteraction : AInteraction
    {
        [Inject] private InteractionController _interactionController;
        [Inject] private GameConfig _gameConfig;
        [Inject] private IDataStorage<PlayerData> _dataStorage;
        [Inject] private IAssetProvider _assetProvider;
        [Inject] private IUiContainer _uiContainer;
        
        [SerializeField] private GameObject _animationPivot;
        [SerializeField] private Transform _tooltipFirstPivot;
        [SerializeField] private Transform _tooltipSecondPivot;

        private int _currentBricks;
        
        public override Transform TooltipFirstPivot => _tooltipFirstPivot;
        public override Transform TooltipSecondPivot => _tooltipSecondPivot;

        public override bool AllowInteraction(PlayerInteractor interactor) => interactor.IsBusy && interactor.BusyKey == PlayerInteractor.BRICK_KEY;

        private void Awake()
        {
            _interactionController.Interactions.Add(this);
            _currentBricks = _dataStorage.CurrentData.IsNew ? _gameConfig.StartBrickCount : _dataStorage.CurrentData.Bricks;
            for (int i = 1; i <= _currentBricks; i++)
                Build(i);
        }

        public override void Interact(PlayerInteractor interactor)
        {
            interactor.SetEmpty();
            _currentBricks++;
            _dataStorage.CurrentData.Bricks = _currentBricks;
            Build(_currentBricks);
            
            interactor.Player.Animate(_animationPivot.transform, _gameConfig.PlayerColumnAnimation, _gameConfig.PlayerColumnDuration);
            if (_currentBricks >= _gameConfig.TotalBrickCount)
                _uiContainer.Show<FinishController>();
        }

        private void Build(float number)
        {
            var columnPart = _assetProvider.Instantiate<ColumnPartBehaviour>(GameGlobal.PREFABS_BUNDLE, transform);
            columnPart.transform.position = new Vector3(transform.position.x, transform.position.y + number - 1 * _gameConfig.BrickHeight, transform.position.z);
        }
    }
}