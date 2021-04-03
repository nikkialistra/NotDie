using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Entities.Items.Weapon
{
    public class WeaponFacade : MonoBehaviour, IPoolable<WeaponSpecs, IMemoryPool>, IDisposable
    {
        [SerializeField] private ReactiveProperty<Data.Weapon> _weapon;
        [SerializeField] private ReactiveProperty<float> _durability;

        public Data.Weapon Weapon => _weapon.Value;

        public float Durability
        {
            get => _durability.Value;
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException(nameof(Durability));
                _durability.Value = value;
            }
        }

        private IMemoryPool _pool;

        public void OnSpawned(WeaponSpecs weaponSpecs, IMemoryPool pool)
        {
            _pool = pool;

            _weapon.Value = weaponSpecs.Weapon;
            _durability.Value = weaponSpecs.Durability;
        }

        public void Dispose() => _pool.Despawn(this);
        
        public void OnDespawned() => _pool = null;

        public class Factory : PlaceholderFactory<WeaponSpecs, WeaponFacade>
        {
        }
    }
}