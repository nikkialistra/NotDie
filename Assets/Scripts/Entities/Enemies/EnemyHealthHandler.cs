using System;
using System.Collections.Generic;
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
            var waveFacade = other.GetComponentInParent<WaveFacade>();
            if (waveFacade == null) 
                return;

            if (!waveFacade.IsPenetrable)
                if (TakeOnce(waveFacade)) return;

            TakeDamage(waveFacade.DamageValue, waveFacade);
        }

        private bool TakeOnce(WaveFacade wave)
        {
            if (_damagedWaves.Contains(wave.Id))
                return true;

            _damagedWaves.Add(wave.Id);
            return false;
        }

        private void TakeDamage(int damage, WaveFacade waveFacade)
        {
            if (damage <= 0)
                throw new ArgumentException("Damage must be more than zero");
            _value -= damage;
            waveFacade.Hitted();
            Debug.Log(damage);


            if (!isAlive)
                Destroy(gameObject);
        }
    }
}