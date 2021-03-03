using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{ 
    public class WeaponsView
    {
        private Image _leftWeapon;
        private Image _rightWeapon;

        public WeaponsView([Inject(Id = "leftWeapon")] Image leftWeapon, [Inject(Id = "rightWeapon")] Image rightWeapon)
        {
            _leftWeapon = leftWeapon;
            _rightWeapon = rightWeapon;
        }

        public void SetLeftWeaponSprite(Sprite weapon) => _leftWeapon.sprite = weapon;

        public void SetRightWeaponSprite(Sprite weapon) => _rightWeapon.sprite = weapon;
    }
}