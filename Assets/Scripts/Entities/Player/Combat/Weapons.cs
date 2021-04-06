using System;
using Items.Weapon;
using UnityEngine;
using Zenject;

namespace Entities.Player.Combat
{
    [RequireComponent(typeof(WeaponFacade))]
    public class Weapons
    {
        public event Action LeftWeaponChanged;
        public event Action RightWeaponChanged;

        public event Action LeftWeaponIsActive;
        public event Action RightWeaponIsActive;

        private WeaponFacade _hand;
        private WeaponFacade _crossedSlot;

        public WeaponFacade ActiveWeapon => _leftIsActive ? _leftWeapon : _rightWeapon;
        public WeaponFacade NotActiveWeapon => _leftIsActive ? _rightWeapon : _leftWeapon;
        
        public WeaponFacade LeftWeapon => _leftWeapon;
        public WeaponFacade RightWeapon => _rightWeapon;

        public bool LeftIsActive => _leftIsActive;

        public bool HandIsActive => ActiveWeapon == _hand;

        private bool IsBothSlotsFree => _leftWeapon == _hand && _rightWeapon == _hand;

        private WeaponFacade _leftWeapon;
        private WeaponFacade _rightWeapon;

        private bool _leftIsActive;

        public Weapons([Inject(Id = "hand")] WeaponFacade hand, [Inject(Id = "crossedSlot")] WeaponFacade crossedSlot)
        {
            _hand = hand;
            _crossedSlot = crossedSlot;

            _leftWeapon = _hand;
            _rightWeapon = _hand;
        }

        public void TakeWeapon(WeaponFacade weaponFacade, out WeaponFacade firstDiscardedWeapon, out WeaponFacade secondDiscardedWeapon)
        {
            if (weaponFacade == null)
                throw new ArgumentNullException(nameof(weaponFacade));

            firstDiscardedWeapon = null;
            secondDiscardedWeapon = null;

            if (weaponFacade.Weapon.IsTwoHanded)
            {
                FreeSlotsIfNeeded(ref firstDiscardedWeapon, ref secondDiscardedWeapon);
                TakeTwoHandedWeapon(weaponFacade);
                return;
            }

            if (!TryTakeInsteadOfHands(weaponFacade))
                firstDiscardedWeapon = SwapWeapon(weaponFacade);
        }

        public WeaponFacade DropWeapon()
        {
            if (ActiveWeapon.Weapon.IsTwoHanded)
                return DropTwoHandedWeapon();
            
            return _leftIsActive ? DropLeftWeapon() : DropRightWeapon();
        }

        public void SwapWeapons()
        {
            if (ActiveWeapon.Weapon.IsTwoHanded)
                return;
            
            _leftIsActive = !_leftIsActive;
            
            if (_leftIsActive)
                LeftWeaponIsActive?.Invoke();
            else
                RightWeaponIsActive?.Invoke();
        }

        private void FreeSlotsIfNeeded(ref WeaponFacade firstDiscardedWeapon, ref WeaponFacade secondDiscardedWeapon)
        {
            if (IsBothSlotsFree)
                return;

            firstDiscardedWeapon = DropLeftWeapon();
            secondDiscardedWeapon = DropRightWeapon();

            if (firstDiscardedWeapon != null) 
                return;
            
            firstDiscardedWeapon = secondDiscardedWeapon;
            secondDiscardedWeapon = null;
        }

        private void TakeTwoHandedWeapon(WeaponFacade weaponFacade)
        {
            ChangeActiveWeapon(weaponFacade);
            ChangeNotActiveWeapon(_crossedSlot);
        }

        private WeaponFacade DropTwoHandedWeapon()
        {
            WeaponFacade droppedWeapon;
            if (_leftIsActive)
            {
                droppedWeapon = DropLeftWeapon();
                _rightWeapon = _hand;
                RightWeaponChanged?.Invoke();
            }
            else
            {
                droppedWeapon = DropRightWeapon();
                _leftWeapon = _hand;
                LeftWeaponChanged?.Invoke();
            }

            return droppedWeapon;
        }

        private WeaponFacade DropLeftWeapon()
        {
            var droppedWeapon = _leftWeapon;
            if (droppedWeapon == _hand || droppedWeapon == _crossedSlot)
                return null;

            _leftWeapon.DurabilityChanged -= LeftWeaponDurabilityChanged;
            _leftWeapon = _hand;
            
            LeftWeaponChanged?.Invoke();

            if (_rightWeapon == _crossedSlot)
                _rightWeapon = _hand;

            return droppedWeapon;
        }

        private WeaponFacade DropRightWeapon()
        {
            var droppedWeapon = _rightWeapon;
            if (droppedWeapon == _hand || droppedWeapon == _crossedSlot)
                return null;

            _rightWeapon.DurabilityChanged -= RightWeaponDurabilityChanged;
            _rightWeapon = _hand;
            
            RightWeaponChanged?.Invoke();

            if (_leftWeapon == _crossedSlot)
                _leftWeapon = _hand;

            return droppedWeapon;
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
            
            if (discardedWeapon.Weapon.IsTwoHanded) 
                SwapTwoHandedWeapon();

            return discardedWeapon;
        }

        private void SwapTwoHandedWeapon()
        {
            if (_leftIsActive)
            {
                _rightWeapon = _hand;
                RightWeaponChanged?.Invoke();
            }
            else
            {
                _leftWeapon = _hand;
                LeftWeaponChanged?.Invoke();
            }
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