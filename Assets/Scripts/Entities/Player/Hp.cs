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
        public int Lives => _lives;

        private int _lives;
        private int _healthValue;

        private bool IsAlive => _settings.Lives > 0;
        
        public Hp(Settings settings)
        {
            _settings = settings;
            _healthValue = _settings.HealthFullValue;
            _lives = _settings.Lives;
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
            else
                HealthChanged?.Invoke(_healthValue);
        }

        private void TakeAwayLive()
        {
            if (_lives <= 0)
                throw new InvalidOperationException("It should not be invoked when lives not greater than 0");
            
            _lives -= 1;
            LivesChanged?.Invoke(_lives);

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