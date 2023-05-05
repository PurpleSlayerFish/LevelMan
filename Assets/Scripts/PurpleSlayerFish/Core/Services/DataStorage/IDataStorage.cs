using PurpleSlayerFish.Core.Data;

namespace PurpleSlayerFish.Core.Services.DataStorage
{
    public interface IDataStorage<T> where T : class, IStorableData, new()
    {
        ref T CurrentData { get; }
        void Save(T data);
        void SaveCurrent();
        T Load();
        void LoadCurrent();
        void Clear();
    }
}