using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace PurpleSlayerFish.Core.Ui.ElementManager.Elements
{
    public class ExternalCheckBox : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _check;

        private bool _isChecked = true;
        
        public void AddOnClick([NotNull] Action<bool> action) => _button.onClick.AddListener(() =>
        {
            SetCheck(!_isChecked);
            action.Invoke(_isChecked);
        });

        public void SetCheck(bool value)
        {
            _isChecked = value;
            _check.enabled = _isChecked;
        }
    }
}