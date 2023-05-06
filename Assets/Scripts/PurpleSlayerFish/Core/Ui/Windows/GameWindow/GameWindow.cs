using PurpleSlayerFish.Core.Ui.ElementManager.Elements;
using TMPro;
using UnityEngine;

namespace PurpleSlayerFish.Core.Ui.Windows.GameWindow
{
    public class GameWindow : AbstractWindow<GameController>
    {
        public TMP_Text Score;
        public Bar Bar;
        public ExtendedButton Tutorial1Complete;
        public ExtendedButton Tutorial2Complete;
        public GameObject Tutorial1;
        public GameObject Tutorial2;
    }
}