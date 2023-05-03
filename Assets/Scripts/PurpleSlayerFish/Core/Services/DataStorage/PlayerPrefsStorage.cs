using Newtonsoft.Json;
using UnityEngine;

namespace PurpleSlayerFish.Core.Services.DataStorage
{
    public class PlayerPrefsStorage<T> : IDataStorage<T> where T : new()
    {
        private string _jsonData;
        
        public void Save(T data) => PlayerPrefs.SetString(typeof(T).Name, JsonConvert.SerializeObject(data));

        // todo loading and save cache
        public T Load()
        {
            _jsonData = PlayerPrefs.GetString(typeof(T).Name);
            if (_jsonData == "")
                return new T();
            return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(typeof(T).Name));
        }

        public void Clear() => PlayerPrefs.DeleteKey(typeof(T).Name);
    }
}