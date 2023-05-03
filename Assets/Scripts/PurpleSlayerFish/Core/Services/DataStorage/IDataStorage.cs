namespace PurpleSlayerFish.Core.Services.DataStorage
{
    public interface IDataStorage<T>
    {
        void Save(T data);
        T Load();
        void Clear();
    }
}