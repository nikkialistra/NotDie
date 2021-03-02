using System;
using Entities.Data;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class Hp : MonoBehaviour
    {
        public event Action<int> HealthChanged;

        public event Action<int> LivesChanged;

        public event Action GameOver;
        
        [Serializable]
        public class Settings
        {
            public int HealthFullValue;
            public int Lives;

            [Header("Audio")]
            public Sound LiveTakenAway;
            public Sound GameOver;
        }

        private Settings _settings;

        public int HealthFullValue => _settings.HealthFullValue;

        public int Lives => _settings.Lives;

        private int _healthValue;

        private bool IsAlive => _settings.Lives > 0;
        
        [Inject]
        public void Construct(Settings settings) => _settings = settings;

        private void Awake()
        {
            _settings.LiveTakenAway.CreateAudioSource(gameObject);
            _settings.GameOver.CreateAudioSource(gameObject);
        }

        private void Start() => _healthValue = _settings.HealthFullValue;

        [ContextMenu("Take damage")]
        private void TakeDamageContextMenu() => TakeDamage(50);

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                throw new ArgumentException("Damage must be more than zero");
            
            if (_healthValue <= 0)
                throw new InvalidOperationException("Health should not be 0 or less");
            
            _healthValue -= damage;
            HealthChanged?.Invoke(_healthValue);

            if (_healthValue <= 0) 
                TakeAwayLive();
        }

        private void TakeAwayLive()
        {
            if (_settings.Lives <= 0)
                throw new InvalidOperationException("It should not be invoked when lives not greater than 0");
            
            _settings.Lives -= 1;
            _settings.LiveTakenAway.Play();
            LivesChanged?.Invoke(_settings.Lives);

            if (IsAlive)
            {
                _healthValue = _settings.HealthFullValue;
                HealthChanged?.Invoke(_healthValue);
            }
            else
            {
                _settings.GameOver.Play();
                GameOver?.Invoke();
            }
        }
    }
}