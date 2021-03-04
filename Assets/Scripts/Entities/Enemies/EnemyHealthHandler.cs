using System;
using Entities.Wave;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    public class EnemyHealthHandler : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public int Value;
        }

        private int _value;

        private bool isAlive => _value > 0;
        
        [Inject]
        public void Construct(Settings settings) => _value = settings.Value;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var wave = other.GetComponent<WaveFacade>();
            if (wave == null) 
                return;

            wave.Dispose();
            TakeDamage(wave.DamageValue);
        }

        private void TakeDamage(int damage)
        {
            if (damage <= 0)
                throw new ArgumentException("Damage must be more than zero");
            _value -= damage;


            if (!isAlive)
                Destroy(gameObject);
        }
    }
}