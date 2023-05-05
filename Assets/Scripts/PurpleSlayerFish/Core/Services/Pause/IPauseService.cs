namespace PurpleSlayerFish.Core.Services.Pause
{
    public interface IPauseService
    {
        bool IsPaused { get; set; }
        void SetPause(bool value);
        void UpdatePauseWindow();
    }
}