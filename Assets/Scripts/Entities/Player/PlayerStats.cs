using System;
using System.Collections.Generic;
using Core.StatSystem;
using Entities.Player.Items;
using Things.Item;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(InventoryHandler))]
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
        
        private InventoryHandler _inventoryHandler;

        public Stat HealthFull { get; private set; }
        public Stat Speed { get; private set; }
        public Stat Damage { get; private set; }
        public Stat Agility { get; private set; }
        public Stat Fortune { get; private set; }
        public Stat Armor { get; private set; }
        public Stat Spikes { get; private set; }
        public Stat Vampirism { get; private set; }
        public Stat Regeneration { get; private set; }

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
        }

        private void Awake()
        {
            _inventoryHandler = GetComponent<InventoryHandler>();

            HealthFull = new Stat(_settings.BaseHealthFull);
            Speed = new Stat(_settings.BaseSpeed);
            Damage = new Stat(0);
            Agility = new Stat(0);
            Fortune = new Stat(0);
            Armor = new Stat(0);
            Spikes = new Stat(0);
            Vampirism = new Stat(0);
            Regeneration = new Stat(0);
        }

        private void OnEnable()
        {
            _inventoryHandler.InventoryChange += OnInventoryChange;
        }
        
        private void OnDisable()
        {
            _inventoryHandler.InventoryChange -= OnInventoryChange;
        }

        private void OnInventoryChange(ItemFacade itemFacade)
        {
            if (itemFacade.TryGetStatModifiers(out List<StatModifier> statModifiers))
            {
                foreach (var statModifier in statModifiers)
                {
                    var stat = statModifier.StatType switch
                    {
                        StatType.FullHealth => HealthFull,
                        StatType.Speed => Speed,
                        StatType.Damage => Damage,
                        StatType.Agility => Agility,
                        StatType.Fortune => Fortune,
                        StatType.Armor => Armor,
                        StatType.Spikes => Spikes,
                        StatType.Vampirism => Vampirism,
                        StatType.Regeneration => Regeneration,
                        _ => throw new ArgumentOutOfRangeException(nameof(statModifier.StatType))
                    };
                    
                    stat.AddModifier(statModifier);
                }
            }
        }
    }
}