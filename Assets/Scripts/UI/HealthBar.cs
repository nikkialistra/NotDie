using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _fillIndicator;

        [SerializeField] private float _fullValue;

        private float _value;

        private void Start()
        {
            _value = _fullValue;
        }

        public void SetValue(float value)
        {
            _value = value;
            
            _fillIndicator.fillAmount = _value / _fullValue;
        }

    }
}