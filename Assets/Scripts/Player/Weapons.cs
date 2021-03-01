﻿using System;
using Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class Weapons : MonoBehaviour
    {
        public event Action LeftWeaponChanged; 
        public event Action RightWeaponChanged;

        public event Action LeftWeaponIsActive;
        public event Action RightWeaponIsActive;

        [SerializeField] private Weapon _hand;

        public Weapon LeftWeapon => _leftWeapon;
        public Weapon RightWeapon => _rightWeapon;
        
        public bool LeftIsActive => _leftIsActive;

        private Weapon _leftWeapon;
        private Weapon _rightWeapon;
        
        private bool _leftIsActive;
        
        private PlayerInput _input;
        private InputAction _swapWeaponsAction;
        
        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _swapWeaponsAction = _input.actions.FindAction("SwapWeapons");
        }

        private void OnEnable() => _swapWeaponsAction.started += OnSwapWeapons;

        private void OnDisable() => _swapWeaponsAction.started -= OnSwapWeapons;

        private void Start()
        {
            _leftWeapon = _hand;
            _rightWeapon = _hand;
        }

        public Weapon TakeWeapon(Weapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon));
            
            var activeWeapon = _leftIsActive ? _leftWeapon : _rightWeapon;
            var notActiveWeapon = _leftIsActive ? _rightWeapon : _leftWeapon;

            if (TryTakeInsteadOfHands(weapon, activeWeapon, notActiveWeapon))
                return null;

            return SwapWeapon(weapon, activeWeapon);
        }

        public Weapon DropWeapon()
        {
            Weapon droppedWeapon;
            if (_leftIsActive)
            {
                droppedWeapon = _leftWeapon;
                if (droppedWeapon != _hand)
                {
                    _leftWeapon = _hand;
                    LeftWeaponChanged?.Invoke();
                    
                    return droppedWeapon;
                }
            }
            else
            {
                droppedWeapon = _rightWeapon;
                if (droppedWeapon != _hand)
                {
                    _rightWeapon = _hand;
                    RightWeaponChanged?.Invoke();
                    
                    return droppedWeapon;
                }
            }
            
            return null;
        }

        private void OnSwapWeapons(InputAction.CallbackContext context)
        {
            _leftIsActive = !_leftIsActive;
            
            if (_leftIsActive)
                LeftWeaponIsActive?.Invoke();
            else
                RightWeaponIsActive?.Invoke();
        }

        private bool TryTakeInsteadOfHands(Weapon weapon, Weapon activeWeapon, Weapon notActiveWeapon)
        {
            if (activeWeapon == _hand)
            {
                ChangeActiveWeapon(weapon);
                return true;
            }
            
            if (notActiveWeapon == _hand)
            {
                ChangeNotActiveWeapon(weapon);
                return true;
            }
            
            return false;
        }

        private Weapon SwapWeapon(Weapon weapon, Weapon activeWeapon)
        {
            var discardedWeapon = activeWeapon;
            ChangeActiveWeapon(weapon);

            return discardedWeapon;
        }

        private void ChangeActiveWeapon(Weapon weapon)
        {
            if (_leftIsActive)
            {
                _leftWeapon = weapon;
                LeftWeaponChanged?.Invoke();
            }
            else
            {
                _rightWeapon = weapon;
                RightWeaponChanged?.Invoke();
            }
        }
        
        private void ChangeNotActiveWeapon(Weapon weapon)
        {
            if (_leftIsActive)
            {
                _rightWeapon = weapon;
                RightWeaponChanged?.Invoke();
            }
            else
            {
                _leftWeapon = weapon;
                LeftWeaponChanged?.Invoke();
            }
        }
    }
}