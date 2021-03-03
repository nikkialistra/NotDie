using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Data
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
    public class Weapon : ScriptableObject
    {
        [Header("Icons")] 
        public Sprite PickUp;
        
        public Sprite Active;
        public Sprite NotActive;
        
        [Header("Specs")]
        public List<WaveSpecs> Waves;

        public float CooldownTime;

        [Serializable]
        public class WaveSpecs
        {
            public int Damage;
            public float TimeToLive;
        }
    }
}