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

        public float LeftWeaponDurability => _leftWeaponDurability;
        public float RightWeaponDurability => _rightWeaponDurability;

        public bool LeftIsActive => _leftIsActive;

        private Weapon _leftWeapon;
        private Weapon _rightWeapon;

        private float _leftWeaponDurability = 1;
        private float _rightWeaponDurability = 1;
        
        private bool _leftIsActive;

        public Weapons(Weapon hand)
        {
            _hand = hand;
            
            _leftWeapon = _hand;
            _rightWeapon = _hand;
        }

        public Weapon TakeWeapon(Weapon weapon, float durability)
        {
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon));

            if (TryTakeInsteadOfHands(weapon, durability))
                return null;

            return SwapWeapon(weapon, durability);
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

        private bool TryTakeInsteadOfHands(Weapon weapon, float durability)
        {
            if (ActiveWeapon == _hand)
            {
                ChangeActiveWeapon(weapon, durability);
                return true;
            }
            
            if (NotActiveWeapon == _hand)
            {
                ChangeNotActiveWeapon(weapon, durability);
                return true;
            }
            
            return false;
        }

        private Weapon SwapWeapon(Weapon weapon, float durability)
        {
            var discardedWeapon = ActiveWeapon;
            ChangeActiveWeapon(weapon, durability);
            
            return discardedWeapon;
        }

        private void ChangeActiveWeapon(Weapon weapon, float durability)
        {
            if (_leftIsActive)
            {
                _leftWeapon = weapon;
                _leftWeaponDurability = durability;
                LeftWeaponChanged?.Invoke();
            }
            else
            {
                _rightWeapon = weapon;
                _rightWeaponDurability = durability;
                RightWeaponChanged?.Invoke();
            }
        }
        
        private void ChangeNotActiveWeapon(Weapon weapon, float durability)
        {
            if (_leftIsActive)
            {
                _rightWeapon = weapon;
                _rightWeaponDurability = durability;
                RightWeaponChanged?.Invoke();
            }
            else
            {
                _leftWeapon = weapon;
                _leftWeaponDurability = durability;
                LeftWeaponChanged?.Invoke();
            }
        }
    }
}