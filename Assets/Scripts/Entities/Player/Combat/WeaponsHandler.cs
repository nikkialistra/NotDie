using System;
using Entities.Data;
using Entities.Items.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Zenject;

namespace Entities.Player.Combat
{
    [RequireComponent(typeof(PlayerInput))]
    public class WeaponsHandler : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public Weapon Hand;

            public float DistanceForTaking;
        }

        public bool AnyWeaponActive => !_weapons.HandIsActive;
        public WeaponFacade ActiveWeapon => _weapons.ActiveWeapon;
        
        private Settings _settings;

        private Weapons _weapons;

        private WeaponGameObjectSpawner _weaponGameObjectSpawner;

        private PlayerInput _input;
        private InputAction _takeDropWeaponAction;
        private InputAction _swapWeaponsAction;


        [Inject]
        public void Construct(Settings settings, Weapons weapons, WeaponGameObjectSpawner weaponGameObjectSpawner)
        {
            _weapons = weapons;
            _settings = settings;
            _weaponGameObjectSpawner = weaponGameObjectSpawner;
        }

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _takeDropWeaponAction = _input.actions.FindAction("TakeDropThrowingWeapon");
            _swapWeaponsAction = _input.actions.FindAction("SwapWeapons");
        }

        private void OnEnable()
        {
            _takeDropWeaponAction.canceled += OnTakeDropWeapon;
            _swapWeaponsAction.started += OnSwapWeapons;
        }

        private void OnDisable()
        {
            _takeDropWeaponAction.canceled -= OnTakeDropWeapon;
            _swapWeaponsAction.started -= OnSwapWeapons;
        }
        
        public WeaponFacade TryTakeOffWeapon()
        {
            var weapon = _weapons.DropWeapon();
            return weapon;
        }

        private void OnTakeDropWeapon(InputAction.CallbackContext context)
        {
            if (context.duration > 0.3f)
                return;
            
            if (TryTakeWeapon()) 
                return;

            DropWeapon();
        }

        private void OnSwapWeapons(InputAction.CallbackContext context) => _weapons.SwapWeapons();

        private bool TryTakeWeapon()
        {
            var weapons = FindObjectsOfType<WeaponGameObject>();
            foreach (var weapon in weapons)
            {
                if (Vector3.Distance(transform.position, weapon.transform.position) >
                      _settings.DistanceForTaking) continue;
                
                TakeWeapon(weapon);
                return true;
            }

            return false;
        }

        private void DropWeapon()
        {
            var weapon = _weapons.DropWeapon();
            if (weapon == null) 
                return;
            
            CreateWeapon(weapon);
        }

        private void TakeWeapon(WeaponGameObject weaponGameObject)
        {
            var weaponFacade = weaponGameObject.WeaponFacade;
            weaponGameObject.Dispose();
            
            var discardedWeapon = _weapons.TakeWeapon(weaponFacade);

            if (discardedWeapon == null) 
                return;
            
            CreateWeapon(discardedWeapon);
        }

        private void CreateWeapon(WeaponFacade weaponFacade) => _weaponGameObjectSpawner.Spawn(transform.position, weaponFacade);
    }
}