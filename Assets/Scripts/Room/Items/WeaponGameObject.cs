using System;
using Data;
using UnityEngine;

namespace Room.Items
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

        private void Start()
        {
            _renderer.sprite =  _weapon.Active;
        }

        public void SetWeapon(Weapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon));

            _weapon = weapon;
            
            _renderer.sprite =  _weapon.Active;
        }
    }
}