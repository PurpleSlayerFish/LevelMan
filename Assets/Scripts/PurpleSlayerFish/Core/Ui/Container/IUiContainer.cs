using System;
using PurpleSlayerFish.Core.Ui.Dialogs;
using PurpleSlayerFish.Core.Ui.Windows;

namespace PurpleSlayerFish.Core.Ui.Container
{
    public interface IUiContainer : IDisposable
    {
        void InitializeWindow<T>() where T : AbstractWindow;
        T Get<T>() where T : AbstractController;
        void Show<T>() where T : AbstractController;
        void Hide<T>() where T : AbstractController;
        void ClearRoot();
        DialogBuilder BuildDialog();
    }
}