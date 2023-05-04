using PurpleSlayerFish.Game.Controllers;
using Zenject;

namespace PurpleSlayerFish.Core.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        private StalagnateController _stalagnateController;
        public override void InstallBindings()
        {
            Container.BindInstance(Container.Instantiate<InteractionController>()).AsSingle();
            Container.BindInstance(Container.Instantiate<RatController>()).AsSingle();
            Container.BindInstance(Container.Instantiate<ObstacleController>()).AsSingle();
            _stalagnateController = Container.Instantiate<StalagnateController>();
            Container.BindInstance(_stalagnateController).AsSingle();
            
            _stalagnateController.SpawnStalagmate("STALAGMATE_1");
            _stalagnateController.SpawnStalagmate("STALAGMATE_2");
            _stalagnateController.SpawnStalagmate("STALAGMATE_3");
        }
    }
}