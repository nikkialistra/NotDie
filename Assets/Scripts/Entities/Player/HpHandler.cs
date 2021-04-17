using System;
using System.Collections;
using System.Collections.Generic;
using Core.Interfaces;
using Entities.Enemies.EnemyWave;
using Entities.Enemies.Species;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class HpHandler : MonoBehaviour, IDamageable
    {
        private Hp _hp;

        private Coroutine _takingDamage;
        
        private readonly IList<(Enemy, int)> _damagedWaves = new List<(Enemy, int)>();

        [Inject]
        public void Construct(Hp hp)
        {
            _hp = hp;
            
            _hp.LivesChanged += OnLivesChanged;
            _hp.GameOver += OnGameOver;
        }

        public void TakeDamage(int value) => _hp.TakeDamage(value);

        public void TakeDamageContinuously(int value, float interval)
        {
            if (_takingDamage != null)
                StopCoroutine(_takingDamage);
            
            _takingDamage = StartCoroutine(TakingDamage(value, interval));
        }

        public void StopTakingDamage()
        {
            if (_takingDamage != null)
                StopCoroutine(_takingDamage);
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
            var enemyWaveFacade = other.GetComponentInParent<EnemyWaveFacade>();
            if (enemyWaveFacade == null) 
                return;

            if (!enemyWaveFacade.IsPenetrable)
                if (TakeOnce(enemyWaveFacade)) return;

            TakeWaveDamage(enemyWaveFacade.DamageValue);
        }

        private bool TakeOnce(EnemyWaveFacade wave)
        {
            if (_damagedWaves.Contains((wave.Enemy, wave.Id)))
                return true;

            _damagedWaves.Add((wave.Enemy, wave.Id));
            return false;
        }

        private void TakeWaveDamage(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Damage must be more than zero");
            _hp.TakeDamage(value);
        }

        private void OnLivesChanged(int lives) { }

        private void OnGameOver() { }
    }
}