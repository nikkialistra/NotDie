using System;
using System.Collections.Generic;
using Entities.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{ 
    public class WeaponsView
    {
        [Serializable]
        public class Settings
        {
            public List<Sprite> ActiveFrames;
            public List<Sprite> NotActiveFrames;
        }

        private Settings _settings;

        private Image _leftWeapon;
        private Image _rightWeapon;

        private Image _leftWeaponFrame;
        private Image _rightWeaponFrame;
        
        public WeaponsView(Settings settings,
            [Inject(Id = "leftWeapon")] Image leftWeapon, [Inject(Id = "rightWeapon")] Image rightWeapon, 
            [Inject(Id = "leftWeaponFrame")] Image leftWeaponFrame, [Inject(Id = "rightWeaponFrame")] Image rightWeaponFrame)
        {
            _settings = settings;
            
            _leftWeapon = leftWeapon;
            _rightWeapon = rightWeapon;
            
            _leftWeaponFrame = leftWeaponFrame;
            _rightWeaponFrame = rightWeaponFrame;
        }

        public void SetLeftWeaponSpriteActive(Weapon weapon, float durability)
        {
            _leftWeapon.sprite = weapon.Active;
            _leftWeaponFrame.sprite = GetSprite(_settings.ActiveFrames, durability);
        }

        public void SetRightWeaponSpriteActive(Weapon weapon, float durability)
        {
            _rightWeapon.sprite = weapon.Active;
            _rightWeaponFrame.sprite = GetSprite(_settings.ActiveFrames, durability);
        }
        
        public void SetLeftWeaponSpriteNotActive(Weapon weapon, float durability)
        {
            _leftWeapon.sprite = weapon.NotActive;
            _leftWeaponFrame.sprite = GetSprite(_settings.NotActiveFrames, durability);
        }

        public void SetRightWeaponSpriteNotActive(Weapon weapon, float durability)
        {
            _rightWeapon.sprite = weapon.NotActive;
            _rightWeaponFrame.sprite = GetSprite(_settings.NotActiveFrames, durability);
        }
        
        private Sprite GetSprite(List<Sprite> sprites, float durability)
        {
            if (durability <= 0 || durability > 1)
                throw new ArgumentException("Value should be between 0 and 1");
            
            var index = (sprites.Count-1) * durability;
            return sprites[(int) index];
        }
    }
}