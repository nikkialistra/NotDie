using System;

namespace Entities.Player
{
    public class Hp
    {
        public event Action<float> HealthChanged;

        public event Action<int> LivesChanged;

        public event Action GameOver;
        
        [Serializable]
        public class Settings
        {
            public int Lives;
        }
        
        public float HealthFullValue
        {
            get => _healthFullValue;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(HealthFullValue));
                }
                
                _healthFullValue = value;
            }
        }
        
        public float HealthValue
        {
            get => _healthValue;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(HealthFullValue));
                }
                
                _healthValue = value;
            }
        }

        public int Lives => _lives;

        private readonly Settings _settings;
        
        private int _lives;

        private float _healthFullValue;
        private float _healthValue;


        private bool IsAlive => _settings.Lives > 0;
        
        public Hp(Settings settings)
        {
            _settings = settings;
            _lives = _settings.Lives;
        }

        public void TakeDamage(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Damage must be more than zero");
            }

            if (_healthValue <= 0)
            {
                throw new InvalidOperationException("Health should not be 0 or less");
            }
            
            _healthValue -= value;

            if (_healthValue > 0)
            {
                HealthChanged?.Invoke(_healthValue);
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
                HealthValue = HealthFullValue;
                HealthChanged?.Invoke(HealthValue);
            }
            else
            {
                GameOver?.Invoke();
            }
        }
    }
}