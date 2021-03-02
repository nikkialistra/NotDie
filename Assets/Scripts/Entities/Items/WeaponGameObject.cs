using System;
using Entities.Data;
using UnityEngine;

namespace Entities.Items
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WeaponGameObject : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        
        public Weapon Weapon => _weapon;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Start() => _renderer.sprite =  _weapon.PickUp;

        
        #if UNITY_EDITOR
        private void OnValidate() => UnityEditor.EditorApplication.delayCall += SetSprite;

        private void SetSprite()
        {
            if (this == null)
                return;
            
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.sprite = _weapon.PickUp;
        }
        #endif

        public void SetWeapon(Weapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon));

            _weapon = weapon;
            
            _renderer.sprite =  _weapon.Active;
        }
    }
}