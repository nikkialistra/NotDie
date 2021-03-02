using System;
using Core;
using Entities.Data;
using Entities.Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private Weapon _hand;
        
        [SerializeField] private GameObject _weaponPrefab;

        [SerializeField] private float _distanceForTaking;

        [Header("Audio")]
        [SerializeField] private Sound _takingWeapon;
        [SerializeField] private Sound _droppingWeapon;
        [SerializeField] private Sound _swappingWeapons;
        
        public Weapons Weapons { get; private set; }

        private PlayerInput _input;
        private InputAction _takeDropWeaponAction;
        private InputAction _swapWeaponsAction;

        private void Awake()
        {
            Weapons = new Weapons(_hand);
            
            _takingWeapon.CreateAudioSource(gameObject);
            _droppingWeapon.CreateAudioSource(gameObject);
            _swappingWeapons.CreateAudioSource(gameObject);
            
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
            _swappingWeapons.MultiplePlay();
        }

        private bool TryTakeWeapon()
        {
            var weapons = FindObjectsOfType<WeaponGameObject>();
            foreach (var weapon in weapons)
            {
                if (Vector3.Distance(transform.position, weapon.transform.position) < _distanceForTaking)
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
                InstantiateWeapon(weapon);
                _droppingWeapon.Play();
            }
        }

        private void TakeWeapon(WeaponGameObject weaponGameObject)
        {
            var discardedWeapon = Weapons.TakeWeapon(weaponGameObject.Weapon);
            Destroy(weaponGameObject.gameObject);
            _takingWeapon.Play();

            if (discardedWeapon != null)
            {
                InstantiateWeapon(discardedWeapon);
                _droppingWeapon.Play();
            }
        }

        private void InstantiateWeapon(Weapon weaponToCreate)
        {
            var weapon = Instantiate(_weaponPrefab, transform.position, Quaternion.identity);

            var weaponGameObject = weapon.GetComponent<WeaponGameObject>();
            weaponGameObject.SetWeapon(weaponToCreate);
        }
    }
}