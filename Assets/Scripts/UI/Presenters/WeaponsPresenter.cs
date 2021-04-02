using Entities.Player;
using Entities.Player.Combat;
using UI.Views;

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
                _view.SetLeftWeaponSpriteActive(_weapons.LeftWeapon, _weapons.LeftWeaponDurability);
                _view.SetRightWeaponSpriteNotActive(_weapons.RightWeapon, _weapons.RightWeaponDurability);
            }
            else
            {
                _view.SetLeftWeaponSpriteNotActive(_weapons.LeftWeapon, _weapons.LeftWeaponDurability);
                _view.SetRightWeaponSpriteActive(_weapons.RightWeapon, _weapons.RightWeaponDurability);
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
                _view.SetLeftWeaponSpriteActive(_weapons.LeftWeapon, _weapons.LeftWeaponDurability);
            else
                _view.SetLeftWeaponSpriteNotActive(_weapons.LeftWeapon, _weapons.LeftWeaponDurability);
        }

        private void OnRightWeaponChanged()
        {
            if (_leftIsActive)
                _view.SetRightWeaponSpriteNotActive(_weapons.RightWeapon, _weapons.RightWeaponDurability);
            else
                _view.SetRightWeaponSpriteActive(_weapons.RightWeapon, _weapons.RightWeaponDurability);
        }
    }
}