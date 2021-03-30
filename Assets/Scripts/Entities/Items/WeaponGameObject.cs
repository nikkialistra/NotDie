using System;
using Entities.Data;
using UnityEngine;
using Zenject;

namespace Entities.Items
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WeaponGameObject : MonoBehaviour
    {
        public float Durability
        {
            get => _durability;
            set
            {
                if (value <= 0 || value > 1)
                    throw new ArgumentException("Value should be between 0 and 1");
                _durability = value;
            }
        }

        private float _durability = 1;
        
        [InjectOptional]
        [SerializeField] private Weapon _weapon;

        [InjectOptional] 
        private Vector3? originalPosition;

        public Weapon Weapon => _weapon;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            if (originalPosition != null)
                transform.position = originalPosition.Value;
            
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.sprite =  _weapon.PickUp;
        }

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

        public class Factory : PlaceholderFactory<Weapon, Vector3, WeaponGameObject>
        {
        }
    }
}