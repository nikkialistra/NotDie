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
        
        [HideInInspector] public int HashedTakenName;
        
        [Space]
        public List<ComboShot> ComboShots;

        public float ShotImpulse;
        public float TimeAfterCombo;
        public float CooldownTime;

        [Serializable]
        public class ComboShot
        {
            public AnimationClip Clip;
            [HideInInspector] public int HashedTriggerName;
            
            public AnimationCurve ImpulseCurve;
            
            public AnimationClip WaveClip;
            [HideInInspector] public int HashedWaveTriggerName;
            public int Damage;
            public bool isPenetrable;
            [Range(0, 2)]
            public float WaveDelay;
        }

        private void OnValidate()
        {
            var takenName = name.Substring(0, 1).ToLower() + name.Substring(1) + "Taken";
            HashedTakenName = Animator.StringToHash(takenName);

            Debug.Log($"{takenName} - {HashedTakenName}");
            
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
}