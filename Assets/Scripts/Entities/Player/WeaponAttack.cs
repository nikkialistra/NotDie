using System;
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
            [Range(0, 1)]
            public float WaveCooldownMultiplierFromTimeToLive;

            [Range(0, 1)]
            public float timeToContinueCombo;
        }

        private Settings _settings;
        
        private Weapons _weapons;
        private WaveSpawner _waveSpawner;

        private int _waveNumber;
        
        private float _weaponCooldownFinishingTime;
        private float _waveCooldownFinishingTime;
        private float _wavesResetTime;
        
        private bool OnWeaponCooldown => _weaponCooldownFinishingTime > Time.time;
        private bool OnWaveCooldown => _waveCooldownFinishingTime > Time.time;
        private bool OnWavesResetTime => _wavesResetTime < Time.time;


        [Inject]
        public void Construct(Settings settings, Weapons weapons, WaveSpawner waveSpawner)
        {
            _settings = settings;
            _weapons = weapons;
            _waveSpawner = waveSpawner;
        }

        private void Update()
        {
            if (_waveNumber > 0 && OnWavesResetTime)
            {
                Debug.Log("Combo was reset");
                _waveNumber = 0;
                _weaponCooldownFinishingTime = Time.time + _weapons.ActiveWeapon.CooldownTime;
            }
        }

        public void Attack(Vector3 position, Transform attackDirection)
        {
            if (OnWeaponCooldown)
            {
                Debug.Log($"Weapon cooldown for {_weaponCooldownFinishingTime - Time.time}");
                return;
            }

            if(!TryStartWave(position, attackDirection))
                return;
            
            CompleteWave();
        }

        private bool TryStartWave(Vector3 position, Transform attackDirection)
        {
            if (OnWaveCooldown)
            {
                Debug.Log($"Wave cooldown for {_waveCooldownFinishingTime - Time.time}");
                return false;
            }

            Debug.Log($"Wave number {_waveNumber}");
            _waveSpawner.Spawn(position, attackDirection, _waveNumber);
            
            _waveCooldownFinishingTime = Time.time + _weapons.ActiveWeapon.Waves[_waveNumber].TimeToLive *
                _settings.WaveCooldownMultiplierFromTimeToLive;

            _wavesResetTime = _waveCooldownFinishingTime + _settings.timeToContinueCombo;
            
            return true;
        }

        private void CompleteWave()
        {
            _waveNumber++;
            if (_waveNumber != _weapons.ActiveWeapon.Waves.Count) 
                return;
            
            _waveNumber = 0;
            _weaponCooldownFinishingTime = Time.time + _weapons.ActiveWeapon.CooldownTime;
        }
    }
}