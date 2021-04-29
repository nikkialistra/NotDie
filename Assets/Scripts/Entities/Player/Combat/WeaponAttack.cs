using System.Collections;
using Entities.Player.Animation;
using Entities.Wave;
using Things.Data;
using Things.Weapon;
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
        
        private float _weaponCooldownFinishingTime;

        private int _comboShotNumber;
        private int _waveCounter;

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
            {
                return;
            }

            if (_comboShotNumber == 0 || (_playerAnimator.IsCurrentAnimationWithTag("Transition") &&
                                          _comboShotNumber < _weapons.ActiveWeapon.Weapon.ComboShots.Count))
            {
                StartShot(position, attackDirection);
            }
        }

        private void StartShot(Vector3 position, Transform attackDirection)
        {
            var comboShot = _weapons.ActiveWeapon.Weapon.ComboShots[_comboShotNumber];

            _playerAnimator.PlayAttackAnimation(comboShot.HashedTriggerName);

            _playerMover.AddVelocity(_weapons.ActiveWeapon.Weapon.ShotImpulse, comboShot.ImpulseCurve, comboShot.Clip.length);
            
            _comboShotNumber++;

            StartCoroutine(SpawnWaveAfterDelay(comboShot.WaveDelay, position, attackDirection, comboShot, _weapons.ActiveWeapon));
        }

        private IEnumerator SpawnWaveAfterDelay(float delay, Vector3 position, Transform attackDirection, Weapon.ComboShot comboShot, WeaponFacade weaponFacade)
        {
            yield return new WaitForSeconds(delay);
            
            var waveSpecs = new WaveSpecs
            {
                Id = _waveCounter++,
                WeaponFacade = weaponFacade,
                Transform = attackDirection,
                Direction = (attackDirection.position - position).normalized,
                WaveTriggerName = comboShot.HashedWaveTriggerName,
                Damage = comboShot.Damage,
                isPenetrable = comboShot.isPenetrable,
                Prefab = weaponFacade.Weapon.WavePrefab,
                ReclineValue = weaponFacade.Weapon.ReclineValue
            };
            
            _waveSpawner.Spawn(waveSpecs);
        }

        public void TryMoveInCombo()
        {
            if (_comboShotNumber > 0)
            {
                _playerAnimator.MoveInCombo();
            }
        }

        public void ResetCombo()
        {
            _weaponCooldownFinishingTime = Time.time + _weapons.ActiveWeapon.Weapon.CooldownTime;
            _comboShotNumber = 0;
        }
    }
}