using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using PurpleSlayerFish.Core.Global;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace PurpleSlayerFish.Core.Services.AssetProvider
{
    public class AssetBundleProvider : IAssetProvider
    {
        private const string BUNDLES_FOLDER = "Bundles";

        private Dictionary<string, AssetBundle> _assetBundles;
        private Dictionary<string, Component> _cashedComponents;
        private AssetBundle _assetBundle;
        private DiContainer _container;

        private Component _cashedComponent;
        private string _cashedComponentKey;

        public AssetBundleProvider()
        {
            _assetBundles = new Dictionary<string, AssetBundle>();
            _cashedComponents = new Dictionary<string, Component>();
        }

        public void Install(DiContainer container) => _container = container;

        public T Get<T>(string bundleName, string assetName) where T : Object
        {
            GetAssetBundle(bundleName, out _assetBundle);
            var result = _assetBundle.LoadAsset<T>(assetName);
            if (result == null)
                throw new KeyNotFoundException("There is not prefab with given name '" + assetName + "' in bundle named '" + bundleName + "'.");
            return result;
        }
        
        public Object[] Get(string bundleName)
        {
            GetAssetBundle(bundleName, out _assetBundle);
            var results = _assetBundle.LoadAllAssets();
            if (results == null)
                throw new KeyNotFoundException("There is not prefabs in bundle named '" + bundleName + "'.");
            return results;
        }
        
        public T GetComponentPrefab<T>(string bundleName, string assetName) where T : Component
        {
            _cashedComponentKey = $"{bundleName}:{assetName}:{nameof(T)}";
            if (_cashedComponents.ContainsKey(_cashedComponentKey))
                _cashedComponent =  _cashedComponents[_cashedComponentKey];
            else
            {
                _cashedComponent = Get<GameObject>(bundleName, assetName).GetComponent<T>();
                _cashedComponents.Add(_cashedComponentKey,_cashedComponent);
            }
            return _cashedComponent as T;
        }

        public T GetScriptableObject<T>() where T : Object =>
            Get<T>(GameGlobal.SCRIPTABLE_OBJECTS_BUNDLE, typeof(T).Name);
        public T GetScriptableObject<T>(string scriptableObjectAssetName) where T : Object =>
            Get<T>(GameGlobal.SCRIPTABLE_OBJECTS_BUNDLE, scriptableObjectAssetName);
        public T Instantiate<T>(string bundleName) where T : Component => 
            _container.InstantiatePrefabForComponent<T>(GetComponentPrefab<T>(bundleName, typeof(T).Name));
        public T Instantiate<T>(string bundleName, Transform transform) where T : Component => 
            _container.InstantiatePrefabForComponent<T>(GetComponentPrefab<T>(bundleName, typeof(T).Name), transform);
        public T Instantiate<T>(string bundleName, string assetName) where T : Component => 
            _container.InstantiatePrefabForComponent<T>(GetComponentPrefab<T>(bundleName, assetName));
        public T Instantiate<T>(string bundleName, string assetName, Transform transform) where T : Component => 
            _container.InstantiatePrefabForComponent<T>(GetComponentPrefab<T>(bundleName, assetName), transform);
        public T Instantiate<T>(GameObject prefab) where T : Component => 
            _container.InstantiatePrefabForComponent<T>(prefab);
        public T Instantiate<T>(GameObject prefab, Transform transform) where T : Component => 
            _container.InstantiatePrefabForComponent<T>(prefab, transform);
        
        public void Dispose()
        {
            var enumerator = _assetBundles.GetEnumerator();
            while (enumerator.MoveNext())
                enumerator.Current.Value.Unload(true);
            _assetBundles.Clear();
            _cashedComponents.Clear();
        }
        
        private void GetAssetBundle([NotNull] string bundleName, out AssetBundle assetBundle)
        {
            if (_assetBundles.ContainsKey(bundleName))
                assetBundle = _assetBundles[bundleName];
            else
            {
                assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, BUNDLES_FOLDER + "/" + bundleName));
                if (assetBundle == null)
                    throw new KeyNotFoundException("Failed to load AssetBundle!");
                _assetBundles.Add(bundleName, assetBundle);
            }
        }
    }
}