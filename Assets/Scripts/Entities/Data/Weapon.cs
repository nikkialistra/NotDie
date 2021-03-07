﻿using System;
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
        
        [Space]
        public List<ComboShot> ComboShots;

        public float ShotImpulse;
        public float TimeAfterCombo;
        public float CooldownTime;

        [Serializable]
        public class ComboShot
        {
            public float Time;
            public AnimationCurve ImpulseCurve;
            public int Damage;
            public GameObject Wave;
        }
    }
}