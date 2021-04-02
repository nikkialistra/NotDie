using System.Collections;
using Entities.Data;
using Entities.Player.Animation;
using Entities.Wave;
using UnityEngine;
using Zenject;

namespace Entities.Player.Combat
{
    public class WeaponAttack : MonoBehaviour
    {
        private Weapons _weapons;
        private WaveSpawner _waveSpawner;
        
        private PlayerAnimator _playerAnimator;
        private PlayerMover _playerMover;
        
        private float _weaponCooldownFinishingTime = 0;

        private int _comboShotNumber = 0;
        private int _waveCounter = 0;

        [Inject]
        public void Construct(Weapons weapons, WaveSpawner waveSpawner)
        {
            _weapons = weapons;
            _waveSpawner = waveSpawner;
        }

        private void Awake()
        {
            _playerAnimator = GetComponent<PlayerAnimator>();
            _playerMover = GetComponent<PlayerMover>();
        }

        public void Attack(Vector3 position, Transform attackDirection)
        {
            if (_weaponCooldownFinishingTime > Time.time)
                return;
            
            if (_comboShotNumber == 0 || (_playerAnimator.IsCurrentAnimationWithTag("Transition") &&
                                          _comboShotNumber < _weapons.ActiveWeapon.ComboShots.Count))
                StartShot(position, attackDirection);
        }

        private void StartShot(Vector3 position, Transform attackDirection)
        {
            var comboShot = _weapons.ActiveWeapon.ComboShots[_comboShotNumber];

            _playerAnimator.PlayAttackAnimation(comboShot.HashedTriggerName);

            _playerMover.AddImpulse(_weapons.ActiveWeapon.ShotImpulse, comboShot.ImpulseCurve, comboShot.Clip.length);
            
            _comboShotNumber++;
            
            SpawnWave(position, attackDirection, comboShot);
        }

        private void SpawnWave(Vector3 position, Transform attackDirection, Weapon.ComboShot comboShot)
        {
            var waveSpecs = new WaveSpecs
            {
                Id = _waveCounter++,
                Transform = attackDirection,
                Direction = (attackDirection.position - position).normalized,
                WaveTriggerName = comboShot.HashedWaveTriggerName,
                Damage = comboShot.Damage,
                isPenetrable = comboShot.isPenetrable
            };

            StartCoroutine(AddDelay(comboShot.WaveDelay, waveSpecs));
        }

        private IEnumerator AddDelay(float delay, WaveSpecs waveSpecs)
        {
            yield return new WaitForSeconds(delay);
            
            _waveSpawner.Spawn(waveSpecs);
        }

        public void TryMoveInCombo()
        {
            if (_comboShotNumber > 0)
                _playerAnimator.MoveInCombo();
        }

        public void ResetCombo()
        {
            _weaponCooldownFinishingTime = Time.time + _weapons.ActiveWeapon.CooldownTime;
            _comboShotNumber = 0;
        }
    }
}