using System;
using Entities.Wave;
using UnityEngine;

namespace Entities.Enemies
{
    public class EnemyHealthHandler : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public int Value;
        }

        [SerializeField] private Settings _settings;

        private bool isAlive => _settings.Value > 0;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var wave = other.GetComponent<WaveFacade>();
            if (wave == null) 
                return;

            wave.Disable();
            TakeDamage(wave.DamageValue);
        }

        private void TakeDamage(int damage)
        {
            if (damage <= 0)
                throw new ArgumentException("Damage must be more than zero");
            _settings.Value -= damage;


            if (!isAlive)
                Destroy(gameObject);
        }
    }
}