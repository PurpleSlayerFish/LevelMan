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

        private RatController _ratController;
        private StalagnateController _stalagnateController;
        public override void InstallBindings()
        {
            Container.BindInstance(_assetProvider.GetScriptableObject<RatSpawnConfig>()).AsSingle();
            Container.BindInstance(Container.Instantiate<InteractionController>()).AsSingle();
            // _ratController = Container.Instantiate<RatController>();
            Container.BindInterfacesAndSelfTo<RatController>().AsSingle();
            // Container.BindInstance(_ratController).AsSingle();
            Container.BindInstance(Container.Instantiate<ObstacleController>()).AsSingle();
            _stalagnateController = Container.Instantiate<StalagnateController>();
            Container.BindInstance(_stalagnateController).AsSingle();
        }
    }
}