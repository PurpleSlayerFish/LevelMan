using PurpleSlayerFish.Core.Data;
using PurpleSlayerFish.Core.Services.DataStorage;
using Zenject;

namespace PurpleSlayerFish.Core.Ui.Windows.ShopWindow
{
    public class SettingsController : AbstractController<SettingsWindow>
    {
        [Inject] private IDataStorage<SettingsData> _settingsStorage;

        protected override void AfterInitialize()
        {
            _window.SoundCheck.SetCheck(_settingsStorage.Load().IsSoundEnabled);
            _window.ExitButton.AddOnClick(Hide);
            _window.SoundCheck.AddOnClick(ChangeSound);
        }

        private void ChangeSound(bool value)
        {
            var data = _settingsStorage.Load();
            data.IsSoundEnabled = value;
            _settingsStorage.Save(data);
        }
    }
}