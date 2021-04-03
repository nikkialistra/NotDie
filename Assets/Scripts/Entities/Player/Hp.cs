using System;
using UnityEngine;

namespace Entities.Player
{
    public class Hp
    {
        public event Action<int> HealthChanged;

        public event Action<int> LivesChanged;

        public event Action GameOver;
        
        [Serializable]
        public class Settings
        {
            public int HealthFullValue;
            public int Lives;
        }

        private Settings _settings;

        public int HealthFullValue => _settings.HealthFullValue;
        public int Lives => _settings.Lives;

        private int _healthValue;

        private bool IsAlive => _settings.Lives > 0;
        
        public Hp(Settings settings)
        {
            _settings = settings;
            _healthValue = _settings.HealthFullValue;
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                throw new ArgumentException("Damage must be more than zero");
            
            if (_healthValue <= 0)
                throw new InvalidOperationException("Health should not be 0 or less");
            
            _healthValue -= damage;

            if (_healthValue <= 0)
                TakeAwayLive();
            
            HealthChanged?.Invoke(_healthValue);
        }

        private void TakeAwayLive()
        {
            if (_settings.Lives <= 0)
                throw new InvalidOperationException("It should not be invoked when lives not greater than 0");
            
            _settings.Lives -= 1;
            LivesChanged?.Invoke(_settings.Lives);

            if (IsAlive)
            {
                _healthValue = _settings.HealthFullValue;
                HealthChanged?.Invoke(_healthValue);
            }
            else
            {
                GameOver?.Invoke();
            }
        }
    }
}