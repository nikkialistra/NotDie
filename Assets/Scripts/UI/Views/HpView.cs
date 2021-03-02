using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class HpView : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        [Serializable]
        public class Settings
        {
            public Image FillIndicator;
        
            public Text Lives;
        
            public Text Health;
            public Text HealthShadow;
        }

        private float _fullHealthValue;

        public void SetFullHealthValue(int fullValue) => _fullHealthValue = fullValue;

        public void SetHealth(int value)
        {
            _settings.FillIndicator.fillAmount = value / _fullHealthValue;
            
            _settings.Health.text = value.ToString();
        }

        public void SetLives(int value)
        {
            _settings.Lives.text = value.ToString();
        }
    }
}