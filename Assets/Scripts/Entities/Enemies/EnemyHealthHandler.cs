using System;
using System.Collections;
using System.Collections.Generic;
using Core.Interfaces;
using Entities.Wave;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    public class EnemyHealthHandler : MonoBehaviour, IDamageable
    {
        [Serializable]
        public class Settings
        {
            public int Value;
        }

        private int _value;
        private bool IsAlive => _value > 0;

        private Coroutine _takingDamage;
        
        private readonly IList<int> _damagedWaves = new List<int>();
        
        [Inject]
        public void Construct(Settings settings) => _value = settings.Value;

        public void TakeDamage(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Damage must be more than zero");
            _value -= value;

            if (!IsAlive)
                Destroy(gameObject);
        }

        public void TakeDamageContinuously(int value, float interval)
        {
            if (_takingDamage != null)
                StopCoroutine(_takingDamage);
            
            _takingDamage = StartCoroutine(TakingDamage(value, interval));
        }

        public void StopTakingDamage() => StopCoroutine(_takingDamage);

        private IEnumerator TakingDamage(int value, float interval)
        {
            while (true)
            {
                TakeDamage(value);
                yield return new WaitForSeconds(interval);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var waveFacade = other.GetComponentInParent<WaveFacade>();
            if (waveFacade == null) 
                return;

            if (!waveFacade.IsPenetrable)
                if (TakeOnce(waveFacade)) return;

            TakeWaveDamage(waveFacade.DamageValue, waveFacade);
        }

        private bool TakeOnce(WaveFacade wave)
        {
            if (_damagedWaves.Contains(wave.Id))
                return true;

            _damagedWaves.Add(wave.Id);
            return false;
        }

        private void TakeWaveDamage(int value, WaveFacade waveFacade)
        {
            if (value <= 0)
                throw new ArgumentException("Damage must be more than zero");
            _value -= value;
            waveFacade.Hitted();
            Debug.Log(value);

            if (!IsAlive)
                Destroy(gameObject);
        }
    }
}