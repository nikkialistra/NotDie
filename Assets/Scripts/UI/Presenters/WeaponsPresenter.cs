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
        }

        public void SetUp()
        {
            _view.SetLeftWeaponSpriteNotActive(_weapons.LeftWeapon.Weapon, _weapons.LeftWeapon.Durability);
            _view.SetRightWeaponSpriteActive(_weapons.RightWeapon.Weapon, _weapons.RightWeapon.Durability);

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
                _view.SetLeftWeaponSpriteActive(_weapons.LeftWeapon.Weapon, _weapons.LeftWeapon.Durability);
            else
                _view.SetLeftWeaponSpriteNotActive(_weapons.LeftWeapon.Weapon, _weapons.LeftWeapon.Durability);
        }

        private void OnRightWeaponChanged()
        {
            if (_leftIsActive)
                _view.SetRightWeaponSpriteNotActive(_weapons.RightWeapon.Weapon, _weapons.RightWeapon.Durability);
            else
                _view.SetRightWeaponSpriteActive(_weapons.RightWeapon.Weapon, _weapons.RightWeapon.Durability);
        }
    }
}