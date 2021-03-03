using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class HpView
    {
        private Image _fillIndicator;
        
        private Text _lives;
        private Text _health;

        private float _fullHealthValue;

        public HpView(Image fillIndicator, [Inject(Id = "lives")] Text lives, [Inject(Id = "health")] Text health)
        {
            _fillIndicator = fillIndicator;
            _lives = lives;
            _health = health;
        }

        public void SetFullHealthValue(int fullValue) => _fullHealthValue = fullValue;

        public void SetHealth(int value)
        {
            _fillIndicator.fillAmount = value / _fullHealthValue;
            
            _health.text = value.ToString();
        }

        public void SetLives(int value) => _lives.text = value.ToString();
    }
}