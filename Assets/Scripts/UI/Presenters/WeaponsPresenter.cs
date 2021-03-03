using Entities.Player;
using UI.Views;
using UnityEngine;

namespace UI.Presenters
{
    public class WeaponsPresenter
    {
        private readonly Weapons _weapons;
        private readonly WeaponsView _view;

        private bool _leftIsActive;

        public WeaponsPresenter(Weapons weapons, WeaponsView view)
        {
            _weapons = weapons;
            _view = view;

            _leftIsActive = weapons.LeftIsActive;

            if (_leftIsActive)
            {
                _view.SetLeftWeaponSprite(_weapons.LeftWeapon.Active);
                _view.SetRightWeaponSprite(_weapons.RightWeapon.NotActive);
            }
            else
            {
                _view.SetLeftWeaponSprite(_weapons.LeftWeapon.NotActive);
                _view.SetRightWeaponSprite(_weapons.RightWeapon.Active);
            }

            _weapons.LeftWeaponIsActive += OnLeftWeaponIsActive;
            _weapons.RightWeaponIsActive += OnRightWeaponIsActive;
            
            _weapons.LeftWeaponChanged += OnLeftWeaponChanged;
            _weapons.RightWeaponChanged += OnRightWeaponChanged;
        }

        private void OnLeftWeaponIsActive()
        {
            _leftIsActive = true;
            WeaponsChanged();
        }

        private void OnRightWeaponIsActive()
        {
            _leftIsActive = false;
            WeaponsChanged();
        }

        private void WeaponsChanged()
        {
            OnLeftWeaponChanged();
            OnRightWeaponChanged();
        }

        private void OnLeftWeaponChanged()
        {
            if (_leftIsActive)
                _view.SetLeftWeaponSprite(_weapons.LeftWeapon.Active);
            else
                _view.SetLeftWeaponSprite(_weapons.LeftWeapon.NotActive);
        }

        private void OnRightWeaponChanged()
        {
            if (_leftIsActive)
                _view.SetRightWeaponSprite(_weapons.RightWeapon.NotActive);
            else
                _view.SetRightWeaponSprite(_weapons.RightWeapon.Active);
        }
    }
}