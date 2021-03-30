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

        private void OnLivesChanged(int obj) { }

        private void OnGameOver() { }
    }
}