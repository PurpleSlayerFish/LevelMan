using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PurpleSlayerFish.Core.Services.AssetProvider
{
    public interface IAssetProvider : IDisposable
    {
        T Get<T>(string bundleName, string assetName) where T : Object;
        T GetComponentPrefab<T>(string bundleName, string assetName) where T : Component ;
        Object[] Get(string bundleName);
        T GetScriptableObject<T>() where T : Object;
        T GetScriptableObject<T>(string scriptableObjectAssetName) where T : Object;
        T Instantiate<T>(string bundleName) where T : Component;
        T Instantiate<T>(string bundleName, Transform parent) where T : Component;
        T Instantiate<T>(string bundleName, string assetName) where T : Component;
        T Instantiate<T>(string bundleName, string assetName, Transform parent) where T : Component;
        T Instantiate<T>(GameObject prefab) where T : Component;
        T Instantiate<T>(GameObject prefab, Transform parent) where T : Component;
    }
}