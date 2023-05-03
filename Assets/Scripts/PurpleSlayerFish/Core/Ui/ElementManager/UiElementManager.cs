using PurpleSlayerFish.Core.Global;
using PurpleSlayerFish.Core.Services.AssetProvider;
using PurpleSlayerFish.Core.Ui.ElementManager.Elements;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Core.Ui.ElementManager
{
    public class UiElementManager : IUiElementManager
    {
        [Inject] private IAssetProvider _assetProvider;
        
        public ButtonBuilder BuildButton() => new(Object.Instantiate(
            _assetProvider.GetComponentPrefab<ExtendedButton>(UiGlobal.ELEMENTS_BUNDLE, UiGlobal.BUTTON_PREFAB)));
        public PurchaseBoxBuilder BuildPurchaseBox(bool isBig) => new(Object.Instantiate(
            _assetProvider.GetComponentPrefab<PurchaseBox>(UiGlobal.ELEMENTS_BUNDLE, isBig ? UiGlobal.PURCHASE_BOX_BIG_PREFAB : UiGlobal.PURCHASE_BOX_SMALL_PREFAB)));
    }
}