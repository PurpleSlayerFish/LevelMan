using System.Threading.Tasks;

namespace PurpleSlayerFish.Core.Services.SceneLoader
{
    public interface ISceneLoader
    {
        Task Load(string sceneName);
        
        void Reload();
    }
}