namespace PurpleSlayerFish.Core.Services.Input
{
    public interface IInputProvider<T>
    {
        T Data { get; }
    }
}