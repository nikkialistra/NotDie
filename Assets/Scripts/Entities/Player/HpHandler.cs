using System.Collections;
using Core.Room;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class HpHandler : MonoBehaviour
    {
        private Hp _hp;

        [Inject]
        public void Construct(Hp hp)
        {
            _hp = hp;
            
            _hp.LivesChanged += OnLivesChanged;
            _hp.GameOver += OnGameOver;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var damager = other.GetComponent<Damager>();

            if (damager != null)
                StartCoroutine(TakingDamage(damager.Value));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var damager = other.GetComponent<Damager>();

            if (damager != null)
                StopAllCoroutines();
        }

        private IEnumerator TakingDamage(int damage)
        {
            while (true)
            {
                _hp.TakeDamage(damage);
                yield return new WaitForSeconds(0.5f);
            }
        }

        private void OnLivesChanged(int lives) { }

        private void OnGameOver() { }
    }
}