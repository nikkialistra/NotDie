using System;
using Core.StatSystem;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public int BaseHealthFull;
            [Range(0, 100)]
            public float BaseSpeed;
        }

        private Settings _settings;
        
        public Stat HealthFull;
        public Stat Speed;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
        }

        private void Awake()
        {
            HealthFull.BaseValue = _settings.BaseHealthFull;
            Speed.BaseValue = _settings.BaseSpeed;
        }
    }
}