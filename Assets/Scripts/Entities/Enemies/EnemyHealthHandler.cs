using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private IList<int> _damagedWaves = new List<int>();
        
        [Inject]
        public void Construct(Settings settings) => _value = settings.Value;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var wave = other.GetComponentInParent<WaveFacade>();
            if (wave == null) 
                return;

            if (!wave.IsPenetrable)
                if (TakeOnce(wave)) return;

            TakeDamage(wave.DamageValue);
        }

        private bool TakeOnce(WaveFacade wave)
        {
            if (_damagedWaves.Contains(wave.Id))
                return true;

            _damagedWaves.Add(wave.Id);
            return false;
        }

        private void TakeDamage(int damage)
        {
            if (damage <= 0)
                throw new ArgumentException("Damage must be more than zero");
            _value -= damage;
            Debug.Log(damage);


            if (!isAlive)
                Destroy(gameObject);
        }
    }
}