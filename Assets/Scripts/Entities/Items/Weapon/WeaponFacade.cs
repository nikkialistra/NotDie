using System;
using UnityEngine;
using Zenject;

namespace Entities.Items.Weapon
{
    public class WeaponFacade : MonoBehaviour, IPoolable<WeaponSpecs, IMemoryPool>, IDisposable
    {
        public Data.Weapon Weapon => _weapon;

        private Data.Weapon _weapon;
        private float _durability;
        
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