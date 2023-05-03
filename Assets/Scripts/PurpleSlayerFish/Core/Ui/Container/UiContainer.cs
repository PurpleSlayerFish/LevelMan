using System.Collections.Generic;
using PurpleSlayerFish.Core.Global;
using PurpleSlayerFish.Core.Services.AssetProvider;
using PurpleSlayerFish.Core.Ui.Dialogs;
using PurpleSlayerFish.Core.Ui.ElementManager;
using PurpleSlayerFish.Core.Ui.Windows;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Core.Ui.Container
{
    public class UiContainer : IUiContainer
    {
        [Inject] private IAssetProvider _assetProvider;
        [Inject] private IUiElementManager _uiElementManager;
        [Inject] private UiProvider _uiProvider;
        private Dictionary<string, AbstractController> _uiControllers;
        private DiContainer _container;
        private DialogWindow _dialogPrefab;
        private string _cashedName;

        public void Install(DiContainer container)
        {
            _container = container;
            _uiControllers = new Dictionary<string, AbstractController>();
            _dialogPrefab = _assetProvider.GetComponentPrefab<DialogWindow>(UiGlobal.WINDOWS_BUNDLE, nameof(DialogWindow));
        }

        public void InitializeWindow<T>() where T : AbstractWindow
        {
            AddToDictionary(_assetProvider.Instantiate<AbstractWindow>(
                UiGlobal.WINDOWS_BUNDLE, typeof(T).Name, _uiProvider.RootCanvas.transform ).Initialize(_container));
        }

        public void ClearRoot()
        {
            if (_uiProvider == null)
                return;
            
            Transform child;
            for (int i =  _uiProvider.RootCanvas.transform.childCount - 1; i >= 0; i--)
            {
                child =  _uiProvider.RootCanvas.transform.GetChild(i);
                child.SetParent(null);
                Object.Destroy(child.gameObject);
            }
        }

        public T Get<T>() where T : AbstractController => _uiControllers[typeof(T).Name] as T;
        public void Show<T>() where T : AbstractController => _uiControllers[typeof(T).Name].Show();
        public void Hide<T>() where T : AbstractController => _uiControllers[typeof(T).Name].Hide();
        public DialogBuilder BuildDialog() => 
            new(_assetProvider.Instantiate<DialogWindow>(_dialogPrefab.gameObject, _uiProvider.RootCanvas.transform), _uiElementManager, _container);

        private void AddToDictionary(AbstractController controller)
        {
            _cashedName = controller.GetType().Name;
            _uiControllers.Add(_cashedName, controller);
        }

        public void Dispose() => ClearRoot();
    }
}