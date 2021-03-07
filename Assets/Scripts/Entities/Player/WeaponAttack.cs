using System;
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
        }
        
        public event Action<int> Attacked;
        public event Action<float, AnimationCurve, float> Impulsed; 
        public event Action ComboReset;
        public event Action ComboExit;

        private Settings _settings;
        
        private Weapons _weapons;
        private WaveSpawner _waveSpawner;

        private int _waveNumber;
        
        private float _weaponCooldownFinishingTime;
        private float _waveCooldownFinishingTime;
        private float _wavesResetTime;
        
        private bool OnWeaponCooldown => _weaponCooldownFinishingTime > Time.time;
        private bool OnWaveCooldown => _waveCooldownFinishingTime > Time.time;

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

            if(!TryStartWave(position, attackDirection))
                return;
            
            CompleteWave();
        }

        private bool TryStartWave(Vector3 position, Transform attackDirection)
        {
            if (OnWaveCooldown)
                return false;
            
            Attacked?.Invoke(_waveNumber);

            var curve = _weapons.ActiveWeapon.ComboShots[_waveNumber].ImpulseCurve;
            var timeToBlockControl = _weapons.ActiveWeapon.ComboShots[_waveNumber].Time;
            
            Impulsed?.Invoke(_weapons.ActiveWeapon.ShotImpulse, curve, timeToBlockControl);
            
            _waveSpawner.Spawn(position, attackDirection, _waveNumber);
            
            SetUpNextWaveInterval();
            return true;
        }

        private void SetUpNextWaveInterval()
        {
            if (_delayComboReset != null)
                StopCoroutine(_delayComboReset);
            
            _waveCooldownFinishingTime = Time.time + _weapons.ActiveWeapon.ComboShots[_waveNumber].Time;

            _wavesResetTime = _waveCooldownFinishingTime + _settings.TimeToContinueCombo;

            _delayComboReset = StartCoroutine(DelayComboReset());
        }

        private IEnumerator DelayComboReset()
        {
            yield return new WaitForSeconds(_wavesResetTime - Time.time);
            
            ComboReset?.Invoke();
            
            _waveNumber = 0;
            _weaponCooldownFinishingTime = Time.time + _weapons.ActiveWeapon.CooldownTime;
        }

        private void CompleteWave()
        {
            _waveNumber++;
            if (_waveNumber != _weapons.ActiveWeapon.ComboShots.Count) 
                return;
            
            if (_delayComboReset != null)
                StopCoroutine(_delayComboReset);

            StartCoroutine(DelayComboExit());
        }

        private IEnumerator DelayComboExit()
        {
            _waveCooldownFinishingTime = float.MaxValue;
            
            var delayTime = _weapons.ActiveWeapon.ComboShots[_waveNumber - 1].Time + _weapons.ActiveWeapon.TimeAfterCombo;
            yield return new WaitForSeconds(delayTime);
            
            ComboExit?.Invoke();
            
            _waveNumber = 0;
            _waveCooldownFinishingTime = 0;
            _weaponCooldownFinishingTime = Time.time + _weapons.ActiveWeapon.CooldownTime;
        }
    }
}