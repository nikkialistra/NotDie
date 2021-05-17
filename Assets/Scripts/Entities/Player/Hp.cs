using System;
using UnityEngine;

namespace Entities.Player
{
    public class Hp
    {
        public event Action<float> HealthChanged;
        public event Action<float> HealthFullChanged;

        public event Action<int> LivesChanged;

        public event Action GameOver;
        
        [Serializable]
        public class Settings
        {
            public int Lives;
            public float InitialHealth;
        }
        
        public float HealthFull
        {
            get => _healthFull;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(HealthFull));
                }
                
                _healthFull = value;
                HealthFullChanged?.Invoke(_healthFull);
            }
        }

        public int Lives => _lives;

        private readonly Settings _settings;
        
        private int _lives;

        private float _healthFull;
        private float _health;


        private bool IsAlive => Lives > 0;
        
        public Hp(Settings settings)
        {
            _settings = settings;
            _lives = _settings.Lives;
        }

        public void SetInitialHealth()
        {
            _health = _settings.InitialHealth;
            HealthFullChanged?.Invoke(_health);
            HealthChanged?.Invoke(_health);
        }

        public void TakeDamage(int value)
        {
            if (!IsAlive)
            {
                return;
            }
            
            if (value <= 0)
            {
                throw new ArgumentException("Damage must be more than zero");
            }

            if (_health <= 0)
            {
                throw new InvalidOperationException("Health should not be 0 or less");
            }
            
            _health -= value;

            if (_health > 0)
            {
                HealthChanged?.Invoke(_health);
            }
            else
            {
                TakeAwayLive();
            }
        }

        private void TakeAwayLive()
        {
            if (_lives <= 0)
            {
                throw new InvalidOperationException("It should not be invoked when lives not greater than 0");
            }

            _lives -= 1;
            LivesChanged?.Invoke(_lives);

            if (IsAlive)
            {
                _health = HealthFull;
                HealthChanged?.Invoke(_health);
            }
            else
            {
                _health = 0;
                HealthChanged?.Invoke(_health);
                GameOver?.Invoke();
            }
        }
    }
}