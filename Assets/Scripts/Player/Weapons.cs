using System;
using Data;
using UnityEngine;

namespace Player
{
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

        private void Start()
        {
            _leftWeapon = _hand;
            _rightWeapon = _hand;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                SwapActive();
        }

        public void SwapActive()
        {
            _leftIsActive = !_leftIsActive;
            
            if (_leftIsActive)
                LeftWeaponIsActive?.Invoke();
            else
                RightWeaponIsActive?.Invoke();
        }

        public Weapon TakeWeapon(Weapon weapon)
        {
            var activeWeapon = _leftIsActive ? _leftWeapon : _rightWeapon;
            var notActiveWeapon = _leftIsActive ? _rightWeapon : _leftWeapon;

            if (TryTakeInsteadOfHands(weapon, activeWeapon, notActiveWeapon))
                return null;

            return SwapWeapon(weapon, activeWeapon);
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