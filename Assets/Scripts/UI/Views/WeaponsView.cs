using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{ 
    public class WeaponsView : MonoBehaviour
    {
        [SerializeField] private Settings _settings;

        [Serializable]
        public class Settings
        {
            public Image LeftWeapon;
            public Image RightWeapon;
        }

        public void SetLeftWeaponSprite(Sprite weapon) => _settings.LeftWeapon.sprite = weapon;

        public void SetRightWeaponSprite(Sprite weapon) => _settings.RightWeapon.sprite = weapon;
    }
}