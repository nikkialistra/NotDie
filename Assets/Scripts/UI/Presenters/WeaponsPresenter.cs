using Entities.Player.Combat;
using UI.Views;

namespace UI.Presenters
{
    public class WeaponsPresenter
    {
        private readonly Weapons _weapons;
        private readonly WeaponsView _weaponsView;

        private bool _leftIsActive;

        public WeaponsPresenter(Weapons weapons, WeaponsView weaponsView)
        {
            _weapons = weapons;
            _weaponsView = weaponsView;
        }

        public void SetUp()
        {
            _weaponsView.SetLeftWeaponSpriteNotActive(_weapons.LeftWeapon.Weapon, _weapons.LeftWeapon.Durability);
            _weaponsView.SetRightWeaponSpriteActive(_weapons.RightWeapon.Weapon, _weapons.RightWeapon.Durability);

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
            {
                _weaponsView.SetLeftWeaponSpriteActive(_weapons.LeftWeapon.Weapon, _weapons.LeftWeapon.Durability);
            }
            else
            {
                _weaponsView.SetLeftWeaponSpriteNotActive(_weapons.LeftWeapon.Weapon, _weapons.LeftWeapon.Durability);
            }
        }

        private void OnRightWeaponChanged()
        {
            if (_leftIsActive)
            {
                _weaponsView.SetRightWeaponSpriteNotActive(_weapons.RightWeapon.Weapon, _weapons.RightWeapon.Durability);
            }
            else
            {
                _weaponsView.SetRightWeaponSpriteActive(_weapons.RightWeapon.Weapon, _weapons.RightWeapon.Durability);
            }
        }
    }
}