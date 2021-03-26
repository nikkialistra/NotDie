﻿using System;
using System.Collections;
using Entities.Wave;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    public class WeaponAttack : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            [Range(0, 2)]
            public float TimeToContinueCombo;
            [Range(1, 2)] 
            public float ComboSwitchingTimeMultiplier;
        }
        
        public event Action<int, AnimationClip> Attacked;
        public event Action<float, AnimationCurve, float> Impulsed; 
        public event Action ComboExit;

        private Settings _settings;

        private int _waveCounter;
        
        private Weapons _weapons;
        private WaveSpawner _waveSpawner;

        private int _comboShotNumber;
        
        private float _weaponCooldownFinishingTime;
        private float _waveCooldownFinishingTime;

        private bool OnWeaponCooldown => _weaponCooldownFinishingTime > Time.time;
        private bool OnComboShotCooldown => _waveCooldownFinishingTime > Time.time;

        private Coroutine _delayComboReset;

        [Inject]
        public void Construct(Settings settings, Weapons weapons, WaveSpawner waveSpawner)
        {
            _settings = settings;
            _weapons = weapons;
            _waveSpawner = waveSpawner;
        }

        public void Attack(Vector3 position, Transform attackDirection)
        {
            if (OnWeaponCooldown)
                return;

            if(!TryStartComboShot(position, attackDirection))
                return;
            
            CompleteComboShot();
        }

        private bool TryStartComboShot(Vector3 position, Transform attackDirection)
        {
            if (OnComboShotCooldown)
                return false;

            var comboShot = _weapons.ActiveWeapon.ComboShots[_comboShotNumber];
            
            Attacked?.Invoke(comboShot.HashedTriggerName, comboShot.Clip);

            Impulsed?.Invoke(_weapons.ActiveWeapon.ShotImpulse, comboShot.ImpulseCurve, comboShot.Clip.length);

            var waveSpecs = new WaveSpecs
            {
                Id = _waveCounter++,
                Transform = attackDirection.transform,
                Direction = (attackDirection.position - position).normalized,
                WaveTriggerName = comboShot.HashedWaveTriggerName,
                Damage = comboShot.Damage,
                isPenetrable = comboShot.isPenetrable
            };

            StartCoroutine(SpawnWaveAfterDelay(comboShot.WaveDelay, waveSpecs));

            SetUpComboShotInterval();
            return true;
        }

        private IEnumerator SpawnWaveAfterDelay(float delay, WaveSpecs waveSpecs)
        {
            yield return new WaitForSeconds(delay);
            
            _waveSpawner.Spawn(waveSpecs);
        }

        private void SetUpComboShotInterval()
        {
            if (_delayComboReset != null)
                StopCoroutine(_delayComboReset);

            var comboShotLength = _weapons.ActiveWeapon.ComboShots[_comboShotNumber].Clip.length * _settings.ComboSwitchingTimeMultiplier;
            
            _waveCooldownFinishingTime = Time.time + comboShotLength;

            var wavesResetTime = comboShotLength + _settings.TimeToContinueCombo;

            _delayComboReset = StartCoroutine(DelayComboReset(wavesResetTime));
        }

        private IEnumerator DelayComboReset(float time)
        {
            yield return new WaitForSeconds(time);
            
            ComboExit?.Invoke();
            
            _comboShotNumber = 0;
            _weaponCooldownFinishingTime = Time.time + _weapons.ActiveWeapon.CooldownTime;
        }

        private void CompleteComboShot()
        {
            _comboShotNumber++;
            if (_comboShotNumber != _weapons.ActiveWeapon.ComboShots.Count) 
                return;
            
            if (_delayComboReset != null)
                StopCoroutine(_delayComboReset);

            StartCoroutine(DelayComboExit());
        }

        private IEnumerator DelayComboExit()
        {
            _waveCooldownFinishingTime = float.MaxValue;
            
            var delayTime = _weapons.ActiveWeapon.ComboShots[_comboShotNumber - 1].Clip.length + _weapons.ActiveWeapon.TimeAfterCombo;
            yield return new WaitForSeconds(delayTime);
            
            ComboExit?.Invoke();
            
            _comboShotNumber = 0;
            _waveCooldownFinishingTime = 0;
            _weaponCooldownFinishingTime = Time.time + _weapons.ActiveWeapon.CooldownTime;
        }
    }
}