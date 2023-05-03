using System.Threading.Tasks;
using PurpleSlayerFish.Core.Ui.Windows.LoadingWindow;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace PurpleSlayerFish.Core.Services.SceneLoader
{
    public class AsyncSceneLoader : ISceneLoader
    {
        [Inject] private LoadingController _loadingController;
        private float _startTime;
        
        public async void Load(string sceneName)
        {
            _loadingController.Show();
            
            await Task.Delay(Mathf.RoundToInt(_loadingController.FadeDuration * 1000));
            
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;

            _startTime = Time.time;
            while (asyncOperation.progress < 0.9f && Time.time - _startTime < _loadingController.SimulatedLoadingTime)
                await Task.Yield();
            
            if (Time.time - _startTime < _loadingController.SimulatedLoadingTime)
                await Task.Delay(Mathf.FloorToInt((_loadingController.SimulatedLoadingTime - (Time.time - _startTime)) * 1000));

            _loadingController.Hide();
            asyncOperation.allowSceneActivation = true;
        }
    }
}