using System;
using UnityEngine;
using Wave;

namespace Units
{
    public class HealthHandler : MonoBehaviour
    {
        [SerializeField] private int _value;

        private bool isAlive => _value > 0;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var waveStats = other.GetComponent<WaveStats>();
            if (waveStats == null) return;
            
            Destroy(waveStats.gameObject);
            TakeDamage(waveStats.DamageValue);
        }

        public void TakeDamage(int damage)
        {
            _value -= damage;
            
            if (!isAlive)
                Destroy(gameObject);
        }
    }
}