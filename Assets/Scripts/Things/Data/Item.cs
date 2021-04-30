using System;
using System.Collections.Generic;
using Core.StatSystem;
using UnityEngine;

namespace Things.Data
{
    [CreateAssetMenu(fileName = "Item", menuName = "Data/Item")]
    public class Item : ScriptableObject
    {
        [Serializable]
        public class Level
        {
            public List<StatModifier> StatModifiers = new List<StatModifier>();
        }
        
        [Serializable]
        public class StatModifiers
        {
            public List<float> Values = new List<float>();
        }
        
        public string DescriptionKey;
        
        [Header("Icons")] 
        public Sprite PickUp;
        public Sprite InInventory;

        [Space] 
        public bool HasModifiers;
        
        [Space(30)]
        [Header("StatModifiersGenerator")]
        [Space]
        public List<StatType> StatTypes;
        public List<StatModifierType> StatModifierTypes;

        public List<StatModifiers> StatModifierLevels;
        
        [Space(30)]
        public List<Level> Levels;

        [ContextMenu("Generate Stat Modifiers")]
        public void GenerateModifiers()
        {
            HasModifiers = true;
            Levels = new List<Level>();
            
            var modifierCount = StatTypes.Count;

            if (StatModifierTypes.Count != modifierCount)
            {
                Debug.LogError($"Modifier count is not same on all fields.");
            }

            foreach (var statModifierLevel in StatModifierLevels)
            {
                
                if (statModifierLevel.Values.Count != modifierCount)
                {
                    Debug.LogError("Modifier count is not same on all fields.");
                }
                
                var level = new Level();
                
                for (var i = 0; i < statModifierLevel.Values.Count; i++)
                {
                    var statModifier = new StatModifier()
                    {
                        StatType = StatTypes[i],
                        Value = statModifierLevel.Values[i],
                        Type = StatModifierTypes[i]
                    };
                    
                    level.StatModifiers.Add(statModifier);
                }
                
                Levels.Add(level);
            }
        }

        private void OnValidate()
        {
            if (!HasModifiers)
            {
                Levels = null;
            }
        }
    }
}