using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Data
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
    public class Weapon : ScriptableObject
    {
        [Serializable]
        public class ComboShot
        {
            public AnimationClip Clip;
            [HideInInspector] public int HashedTriggerName;

            public AnimationCurve ImpulseCurve;
            
            public AnimationClip WaveClip;
            [HideInInspector] public int HashedWaveTriggerName;
            public float WaveDelay;
            
            [Space]
            public int Damage;
            public bool isPenetrable;
        }

        [Header("Icons")] 
        public Sprite PickUp;

        public Sprite Active;

        public Sprite NotActive;

        [HideInInspector] public int HashedTakenName;

        [Space]
        public List<ComboShot> ComboShots;

        public GameObject WavePrefab;
        [Range(0.3f, 10f)]
        public float DirectionMultiplier = 1;
        public float ShotImpulse;

        [Space]
        public float CooldownTime;
        public Durability Durability;
        [Range(0.5f, 2)]
        public float DurabilityMultiplier = 1;

        [Space] 
        public int ThrowDamage;

        public float DurabilityLostOnHit()
        {
            return Durability switch
            {
                Durability.Infinity => 0,
                Durability.Strong => 0.05f / DurabilityMultiplier,
                Durability.Moderate => 0.15f / DurabilityMultiplier,
                Durability.Fragile => 0.35f / DurabilityMultiplier,
                Durability.VeryFragile => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(Durability))
            };
        }
        
        private void OnValidate()
        {
            var takenName = name.Substring(0, 1).ToLower() + name.Substring(1) + "Taken";
            HashedTakenName = Animator.StringToHash(takenName);

            foreach (var comboShot in ComboShots)
            {
                if (comboShot.Clip != null)
                {

                    var clipName = comboShot.Clip.name;
                    var triggerName = clipName.Substring(0, 1).ToLower() + clipName.Substring(1);
                    comboShot.HashedTriggerName = Animator.StringToHash(triggerName);
                }
                
                if (comboShot.WaveClip != null)
                {

                    var clipName = comboShot.WaveClip.name;
                    var triggerName = clipName.Substring(0, 1).ToLower() + clipName.Substring(1);
                    comboShot.HashedWaveTriggerName = Animator.StringToHash(triggerName);
                }
            }
        }
    }

    public enum Durability
    {
        Infinity,
        Strong,
        Moderate,
        Fragile,
        VeryFragile
    }
}