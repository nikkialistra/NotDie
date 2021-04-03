using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Entities.Items.Weapon
{
    public class WeaponFacade : MonoBehaviour, IPoolable<WeaponSpecs, IMemoryPool>, IDisposable
    {
        public event Action DurabilityChanged; 
        
        [SerializeField] private Data.Weapon _weapon;
        [SerializeField] private float _durability;

        public Data.Weapon Weapon => _weapon;
        
        [ContextMenu("Decrease durability")]
        void DecreaseDurability()
        {
            Durability -= 0.3f;
        }

        public float Durability
        {
            get => _durability;
            set
            {
                if (value > 1)
                    throw new ArgumentOutOfRangeException(nameof(Durability));
                if (value <= 0)
                    _durability = 0;
                else
                    _durability = value;
                DurabilityChanged?.Invoke();
            }
        }

        private IMemoryPool _pool;

        public void OnSpawned(WeaponSpecs weaponSpecs, IMemoryPool pool)
        {
            _pool = pool;

            _weapon = weaponSpecs.Weapon;
            _durability = weaponSpecs.Durability;
        }

        public void Dispose() => _pool.Despawn(this);
        
        public void OnDespawned() => _pool = null;

        public class Factory : PlaceholderFactory<WeaponSpecs, WeaponFacade>
        {
        }
    }
}