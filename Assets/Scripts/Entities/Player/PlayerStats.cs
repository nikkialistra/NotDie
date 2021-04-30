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
            HealthFull = new Stat(_settings.BaseHealthFull);
            Speed = new Stat(_settings.BaseSpeed);
        }

        [ContextMenu("AddModifier")]
        private void AddModifier()
        {
            var modifier = new StatModifier(10, StatModifierType.Flat);
            HealthFull.AddModifier(modifier);
        }
    }
}