﻿using System;
using Entities.Data;
using Entities.Items;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Entities.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class WeaponHandler : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public Weapon Hand;

            public float DistanceForTaking;

            [Header("Audio")]
            public Sound TakingWeapon;
            public Sound DroppingWeapon;
            public Sound SwappingWeapons;
        }

        private Settings _settings;

        public Weapons Weapons { get; private set; }
        
        private WeaponGameObject.Factory _weaponFactory;

        private PlayerInput _input;
        private InputAction _takeDropWeaponAction;
        private InputAction _swapWeaponsAction;

        [Inject]
        public void Construct(Settings settings, Weapons weapons, WeaponGameObject.Factory weaponFactory)
        {
            Weapons = weapons;
            _settings = settings;
            _weaponFactory = weaponFactory;
            
            _settings.TakingWeapon.CreateAudioSource(gameObject);
            _settings.DroppingWeapon.CreateAudioSource(gameObject);
            _settings.SwappingWeapons.CreateAudioSource(gameObject);
        }

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _takeDropWeaponAction = _input.actions.FindAction("TakeDropWeapon");
            _swapWeaponsAction = _input.actions.FindAction("SwapWeapons");
        }

        private void OnEnable()
        {
            _takeDropWeaponAction.started += OnTakeDropWeapon;
            _swapWeaponsAction.started += OnSwapWeapons;
        }

        private void OnDisable()
        {
            _takeDropWeaponAction.started -= OnTakeDropWeapon;
            _swapWeaponsAction.started -= OnSwapWeapons;
        }

        private void OnTakeDropWeapon(InputAction.CallbackContext context)
        {
            if (TryTakeWeapon()) 
                return;

            DropWeapon();
        }

        private void OnSwapWeapons(InputAction.CallbackContext context)
        {
            Weapons.SwapWeapons();
            _settings.SwappingWeapons.PlayOneShot();
        }

        private bool TryTakeWeapon()
        {
            var weapons = FindObjectsOfType<WeaponGameObject>();
            foreach (var weapon in weapons)
            {
                if (Vector3.Distance(transform.position, weapon.transform.position) < _settings.DistanceForTaking)
                {
                    TakeWeapon(weapon);
                    return true;
                }
            }

            return false;
        }

        private void DropWeapon()
        {
            var weapon = Weapons.DropWeapon();
            if (weapon != null)
            {
                CreateWeapon(weapon);
                _settings.DroppingWeapon.PlayOneShot();
            }
        }

        private void TakeWeapon(WeaponGameObject weaponGameObject)
        {
            var discardedWeapon = Weapons.TakeWeapon(weaponGameObject.Weapon);
            Destroy(weaponGameObject.gameObject);
            _settings.TakingWeapon.PlayOneShot();

            if (discardedWeapon != null)
            {
                CreateWeapon(discardedWeapon);
                _settings.DroppingWeapon.PlayOneShot();
            }
        }

        private void CreateWeapon(Weapon weaponToCreate) => _weaponFactory.Create(weaponToCreate, transform.position);
    }
}