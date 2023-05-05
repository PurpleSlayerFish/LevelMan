using Newtonsoft.Json;
using PurpleSlayerFish.Core.Data;
using UnityEngine;

namespace PurpleSlayerFish.Core.Services.DataStorage
{
    public class PlayerPrefsStorage<T> : IDataStorage<T> where T : class, IStorableData, new()
    {
        private T _currentData;
        private string _jsonData;
        
        public ref T CurrentData => ref _currentData;
        public void Save(T data)
        {
            data.IsNew = false;
            PlayerPrefs.SetString(typeof(T).Name, JsonConvert.SerializeObject(data));
        }

        public void SaveCurrent() => Save(_currentData);

        public T Load()
        {
            _jsonData = PlayerPrefs.GetString(typeof(T).Name);
            if (_jsonData == "")
                return new T();
            return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(typeof(T).Name));
        }
        
        public void LoadCurrent() => _currentData = Load();

        public void Clear()
        {
            _currentData = null;
            PlayerPrefs.DeleteKey(typeof(T).Name);
        }
    }
}