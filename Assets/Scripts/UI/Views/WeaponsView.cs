using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{ 
    public class WeaponsView : MonoBehaviour
    {
        [SerializeField] private Image _leftWeapon;
        [SerializeField] private Image _rightWeapon;

        public void SetLeftWeaponSprite(Sprite weapon) => _leftWeapon.sprite = weapon;
        
        public void SetRightWeaponSprite(Sprite weapon) => _rightWeapon.sprite = weapon;
    }
}