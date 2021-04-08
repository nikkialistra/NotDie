using System;
using System.Collections.Generic;
using Things.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views
{ 
    public class WeaponsView : MonoBehaviour
    {
        [SerializeField] private List<Sprite> ActiveFrames;
        [SerializeField] private List<Sprite> NotActiveFrames;
        
        private VisualElement _rootVisualElement;

        private Image _leftFrame;
        private Image _rightFrame;
        
        private Image _leftWeapon;
        private Image _rightWeapon;
        
        private void Awake()
        {
            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            
            _leftFrame = _rootVisualElement.Q<Image>("left_frame");
            _rightFrame = _rootVisualElement.Q<Image>("right_frame");

            _leftWeapon = _rootVisualElement.Q<Image>("left_weapon");
            _rightWeapon = _rootVisualElement.Q<Image>("right_weapon");
        }

        public void SetLeftWeaponSpriteActive(Weapon weapon, float durability)
        {
            _leftWeapon.sprite = weapon.Active;
            _leftFrame.sprite = GetSprite(ActiveFrames, durability);
        }

        public void SetRightWeaponSpriteActive(Weapon weapon, float durability)
        {
            _rightWeapon.sprite = weapon.Active;
            _rightFrame.sprite = GetSprite(ActiveFrames, durability);
        }
        
        public void SetLeftWeaponSpriteNotActive(Weapon weapon, float durability)
        {
            _leftWeapon.sprite = weapon.NotActive;
            _leftFrame.sprite = GetSprite(NotActiveFrames, durability);
        }

        public void SetRightWeaponSpriteNotActive(Weapon weapon, float durability)
        {
            _rightWeapon.sprite = weapon.NotActive;
            _rightFrame.sprite = GetSprite(NotActiveFrames, durability);
        }
        
        private Sprite GetSprite(List<Sprite> sprites, float durability)
        {
            if (durability <= 0 || durability > 1)
                throw new ArgumentException("Value should be between 0 and 1");

            if (durability > 0.75)
                return sprites[3];
            if (durability > 0.5)
                return sprites[2];
            if (durability > 0.25)
                return sprites[1];
            else
                return sprites[0];
        }
    }
}