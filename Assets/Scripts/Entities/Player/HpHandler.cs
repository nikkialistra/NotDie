using System.Collections;
using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class HpHandler : MonoBehaviour, IDamageable
    {
        private Hp _hp;

        private Coroutine _takingDamage;

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

        private void OnLivesChanged(int lives) { }

        private void OnGameOver() { }
    }
}