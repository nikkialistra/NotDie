using System;
using UnityEngine;
using Wave;

namespace Enemies
{
    public class EnemyHealthHandler : MonoBehaviour
    {
        [SerializeField] private int _value;

        private bool isAlive => _value > 0;

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
            
            _value -= damage;
            
            if (!isAlive)
                Destroy(gameObject);
        }
    }
}