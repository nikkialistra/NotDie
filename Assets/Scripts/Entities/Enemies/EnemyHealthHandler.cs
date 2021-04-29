using System;
using System.Collections;
using System.Collections.Generic;
using Core.Interfaces;
using Entities.Wave;
using UnityEngine;

namespace Entities.Enemies
{
    public class EnemyHealthHandler : MonoBehaviour, IDamageable
    {
        public int Health
        {
            get => _health;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Health));
                }

                _health = value;
            }
        }
        
        public bool ShouldRecline => _reclineUnhandled > 0;

        private int _health;
        
        private bool IsAlive => _health > 0;

        private Coroutine _takingDamage;
        
        private readonly IList<int> _damagedWaves = new List<int>();
        
        private int _reclineUnhandled;

        public int HandleRecline()
        {
            var recline = _reclineUnhandled;
            _reclineUnhandled = 0;
            return recline;
        }

        public void TakeDamage(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Damage must be more than zero");
            _health -= value;

            if (!IsAlive)
            {
                StopTakingDamage();
                Destroy(gameObject);
            }
        }

        public void TakeDamageContinuously(int value, float interval)
        {
            if (_takingDamage != null)
            {
                StopCoroutine(_takingDamage);
            }

            _takingDamage = StartCoroutine(TakingDamage(value, interval));
        }

        public void StopTakingDamage()
        {
            if (_takingDamage != null)
            {
                StopCoroutine(_takingDamage);
            }
        }

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
            {
                return;
            }

            if (!waveFacade.IsPenetrable && TakeOnce(waveFacade))
            {
                return;
            }

            TakeWaveDamage(waveFacade.DamageValue, waveFacade);
        }

        private bool TakeOnce(WaveFacade wave)
        {
            if (_damagedWaves.Contains(wave.Id))
            {
                return true;
            }

            _damagedWaves.Add(wave.Id);
            return false;
        }

        private void TakeWaveDamage(int value, WaveFacade waveFacade)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Damage must be more than zero");
            }

            _health -= value;
            waveFacade.Hitted();
            TakeInRecline(waveFacade);
            
            Debug.Log(value);

            if (!IsAlive)
            {
                Destroy(gameObject);
            }
        }

        private void TakeInRecline(WaveFacade waveFacade)
        {
            _reclineUnhandled += waveFacade.ReclineValue;
        }
    }
}