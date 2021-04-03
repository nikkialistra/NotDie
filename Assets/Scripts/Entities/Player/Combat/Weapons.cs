using System;
using Entities.Items.Weapon;

namespace Entities.Player.Combat
{
    public class Weapons
    {
        public event Action LeftWeaponChanged;
        public event Action RightWeaponChanged;

        public event Action LeftWeaponIsActive;
        public event Action RightWeaponIsActive;

        private WeaponFacade _hand;

        public WeaponFacade ActiveWeapon => _leftIsActive ? _leftWeapon : _rightWeapon;
        public WeaponFacade NotActiveWeapon => _leftIsActive ? _rightWeapon : _leftWeapon;
        
        public WeaponFacade LeftWeapon => _leftWeapon;
        public WeaponFacade RightWeapon => _rightWeapon;

        public bool LeftIsActive => _leftIsActive;

        private WeaponFacade _leftWeapon;
        private WeaponFacade _rightWeapon;

        private bool _leftIsActive;

        public Weapons(WeaponFacade hand)
        {
            _hand = hand;
            
            _leftWeapon = _hand;
            _rightWeapon = _hand;
        }

        public WeaponFacade TakeWeapon(WeaponFacade weaponFacade)
        {
            if (weaponFacade == null)
                throw new ArgumentNullException(nameof(weaponFacade));

            if (TryTakeInsteadOfHands(weaponFacade))
                return null;

            return SwapWeapon(weaponFacade);
        }

        public WeaponFacade DropWeapon()
        {
            WeaponFacade droppedWeapon;
            if (_leftIsActive)
            {
                droppedWeapon = _leftWeapon;
                if (droppedWeapon == _hand)
                    return null;

                _leftWeapon.DurabilityChanged -= LeftWeaponDurabilityChanged;
                _leftWeapon = _hand;
                LeftWeaponChanged?.Invoke();
                    
                return droppedWeapon;
            }
            else
            {
                droppedWeapon = _rightWeapon;
                if (droppedWeapon == _hand) 
                    return null;

                _rightWeapon.DurabilityChanged -= RightWeaponDurabilityChanged;
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

        private bool TryTakeInsteadOfHands(WeaponFacade weaponFacade)
        {
            if (ActiveWeapon == _hand)
            {
                ChangeActiveWeapon(weaponFacade);
                return true;
            }
            
            if (NotActiveWeapon == _hand)
            {
                ChangeNotActiveWeapon(weaponFacade);
                return true;
            }
            
            return false;
        }

        private WeaponFacade SwapWeapon(WeaponFacade weaponFacade)
        {
            var discardedWeapon = ActiveWeapon;
            ChangeActiveWeapon(weaponFacade);
            
            return discardedWeapon;
        }

        private void ChangeActiveWeapon(WeaponFacade weaponFacade)
        {
            if (_leftIsActive)
            {
                SetLeftWeapon(weaponFacade);
                LeftWeaponChanged?.Invoke();
            }
            else
            {
                SetRightWeapon(weaponFacade);
                RightWeaponChanged?.Invoke();
            }
        }

        private void ChangeNotActiveWeapon(WeaponFacade weaponFacade)
        {
            if (_leftIsActive)
            {
                SetRightWeapon(weaponFacade);
                RightWeaponChanged?.Invoke();
            }
            else
            {
                SetLeftWeapon(weaponFacade);
                LeftWeaponChanged?.Invoke();
            }
        }

        private void SetLeftWeapon(WeaponFacade weaponFacade)
        {
            _leftWeapon.DurabilityChanged -= LeftWeaponDurabilityChanged;
            
            _leftWeapon = weaponFacade;
            _leftWeapon.DurabilityChanged += LeftWeaponDurabilityChanged;
        }

        private void SetRightWeapon(WeaponFacade weaponFacade)
        {
            _rightWeapon.DurabilityChanged -= RightWeaponDurabilityChanged;
            
            _rightWeapon = weaponFacade;
            _rightWeapon.DurabilityChanged += RightWeaponDurabilityChanged;
        }

        private void LeftWeaponDurabilityChanged()
        {
            if (LeftWeapon.Durability <= 0)
                DestroyLeftWeapon();

            LeftWeaponChanged?.Invoke();
        }

        private void RightWeaponDurabilityChanged()
        {
            if (RightWeapon.Durability <= 0)
                DestroyRightWeapon();
            
            RightWeaponChanged?.Invoke();
        }

        private void DestroyLeftWeapon()
        {
            _leftWeapon.DurabilityChanged -= LeftWeaponDurabilityChanged;
            _leftWeapon.Dispose();
            _leftWeapon = _hand;
            LeftWeaponChanged?.Invoke();
        }

        private void DestroyRightWeapon()
        {
            _rightWeapon.DurabilityChanged -= RightWeaponDurabilityChanged;
            _rightWeapon.Dispose();
            _rightWeapon = _hand;
            RightWeaponChanged?.Invoke();
        }
    }
}