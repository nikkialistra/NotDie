using System;
using Entities.Data;

namespace Entities.Player
{
    public class Weapons
    {
        public event Action LeftWeaponChanged; 
        public event Action RightWeaponChanged;

        public event Action LeftWeaponIsActive;
        public event Action RightWeaponIsActive;

        private Weapon _hand;

        public Weapon ActiveWeapon => _leftIsActive ? _leftWeapon : _rightWeapon;
        public Weapon NotActiveWeapon => _leftIsActive ? _rightWeapon : _leftWeapon;
        
        public Weapon LeftWeapon => _leftWeapon;
        public Weapon RightWeapon => _rightWeapon;
        
        
        public bool LeftIsActive => _leftIsActive;

        private Weapon _leftWeapon;
        private Weapon _rightWeapon;
        
        private bool _leftIsActive;

        public Weapons(Weapon hand)
        {
            _hand = hand;
            
            _leftWeapon = _hand;
            _rightWeapon = _hand;
        }

        public Weapon TakeWeapon(Weapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon));

            if (TryTakeInsteadOfHands(weapon))
                return null;

            return SwapWeapon(weapon);
        }

        public Weapon DropWeapon()
        {
            Weapon droppedWeapon;
            if (_leftIsActive)
            {
                droppedWeapon = _leftWeapon;
                if (droppedWeapon == _hand) 
                    return null;
                
                _leftWeapon = _hand;
                LeftWeaponChanged?.Invoke();
                    
                return droppedWeapon;
            }
            else
            {
                droppedWeapon = _rightWeapon;
                if (droppedWeapon == _hand) 
                    return null;
                
                _rightWeapon = _hand;
                RightWeaponChanged?.Invoke();
                    
                return droppedWeapon;
            }
        }

        public void SwapWeapons()
        {
            _leftIsActive = !_leftIsActive;
            
            if (_leftIsActive)
                LeftWeaponIsActive?.Invoke();
            else
                RightWeaponIsActive?.Invoke();
        }

        private bool TryTakeInsteadOfHands(Weapon weapon)
        {
            if (ActiveWeapon == _hand)
            {
                ChangeActiveWeapon(weapon);
                return true;
            }
            
            if (NotActiveWeapon == _hand)
            {
                ChangeNotActiveWeapon(weapon);
                return true;
            }
            
            return false;
        }

        private Weapon SwapWeapon(Weapon weapon)
        {
            var discardedWeapon = ActiveWeapon;
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