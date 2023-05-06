using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services.AssetProvider;
using PurpleSlayerFish.Core.Services.DataStorage;
using PurpleSlayerFish.Game.Controllers;
using PurpleSlayerFish.Game.Controllers.Impls;
using PurpleSlayerFish.Game.ScriptableObjects;
using Zenject;

namespace PurpleSlayerFish.Core.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [Inject] private IAssetProvider _assetProvider;

        private StalagnateController _stalagnateController;
        private ObstacleController _obstacleController;
        public override void InstallBindings()
        {
            Container.BindInstance(_assetProvider.GetScriptableObject<RatSpawnConfig>()).AsSingle();
            Container.BindInstance(Container.Instantiate<InteractionController>()).AsSingle();
            Container.BindInterfacesAndSelfTo<RatController>().AsSingle();
            _obstacleController = Container.Instantiate<ObstacleController>();
            Container.BindInstance(_obstacleController).AsSingle();
            _obstacleController.Initialize();
            _stalagnateController = Container.Instantiate<StalagnateController>();
            Container.BindInstance(_stalagnateController).AsSingle();
        }
    }
}