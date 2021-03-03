using System;
using Entities.Data;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class HpHandler : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            [Header("Audio")]
            public Sound LiveTakenAway;
            public Sound GameOver;
        }

        private Settings _settings;
        
        private Hp _hp;

        [Inject]
        public void Construct(Settings settings, Hp hp)
        {
            _settings = settings;
            _hp = hp;
            
            _hp.LivesChanged += OnLivesChanged;
            _hp.GameOver += OnGameOver;
            
            _settings.LiveTakenAway.CreateAudioSource(gameObject);
            _settings.GameOver.CreateAudioSource(gameObject);
        }

        private void OnLivesChanged(int obj) => _settings.LiveTakenAway.Play();

        private void OnGameOver() => _settings.GameOver.Play();
    }
}