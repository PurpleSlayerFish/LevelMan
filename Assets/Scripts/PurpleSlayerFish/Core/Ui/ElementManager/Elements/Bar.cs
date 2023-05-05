using System;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PurpleSlayerFish.Core.Ui.ElementManager.Elements
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Image Image;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _duration;

        private Tweener _tweener;
        
        public void FillAmount(float fillAmount)
        {
            _tweener?.Kill();
            _tweener = Image.DOFillAmount(fillAmount, _duration).SetEase(_ease);
            Debug.Log(fillAmount);
        }
    }
}