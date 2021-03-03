using System;
using Entities.Data;
using UnityEngine;

namespace Entities.Items
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WeaponGameObject : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public Weapon Weapon;
        }

        [SerializeField] private Settings _settings;

        public Weapon Weapon => _settings.Weapon;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Start() => _renderer.sprite =  _settings.Weapon.PickUp;

        
        #if UNITY_EDITOR
        private void OnValidate() => UnityEditor.EditorApplication.delayCall += SetSprite;

        private void SetSprite()
        {
            if (this == null)
                return;
            
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.sprite = _settings.Weapon.PickUp;
        }
        #endif


        public void SetWeapon(Weapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon));

            _settings.Weapon = weapon;
            
            _renderer.sprite =  _settings.Weapon.Active;
        }
    }
}