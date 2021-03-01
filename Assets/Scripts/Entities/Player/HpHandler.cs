using System;
using UnityEngine;

namespace Entities.Player
{
    public class HpHandler : MonoBehaviour
    {
        public event Action<int> HealthChanged;
        public event Action<int> LivesChanged;
        public event Action GameOver;

        [SerializeField] private int _healthFullValue;
        [SerializeField] private int _lives;

        public int HealthFullValue => _healthFullValue;
        public int Lives => _lives;

        private int _healthValue;
        private bool IsAlive => _lives > 0;

        private void Start()
        {
            _healthValue = _healthFullValue;
        }

        [ContextMenu("Take damage")]
        private void TakeDamageContextMenu()
        {
            TakeDamage(50);
        }

        public void TakeDamage(int damage)
        {
            if (damage <= 0)
                throw new ArgumentException("Damage must be more than zero");
            
            _healthValue -= damage;
            HealthChanged?.Invoke(_healthValue);

            if (_healthValue <= 0) 
                TakeAwayLive();
        }

        private void TakeAwayLive()
        {
            _lives -= 1;
            LivesChanged?.Invoke(_lives);

            if (IsAlive)
            {
                _healthValue = _healthFullValue;
                HealthChanged?.Invoke(_healthValue);
            }
            else
            {
                GameOver?.Invoke();
            }
        }
    }
}