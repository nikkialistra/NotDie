﻿using System;
using UnityEngine;
using Zenject;

namespace Entities.Items.Weapon
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WeaponGameObject : MonoBehaviour, IPoolable<Vector3, WeaponFacade, IMemoryPool>, IDisposable
    {
        private WeaponFacade _weaponFacade;

        private SpriteRenderer _renderer;

        private IMemoryPool _pool;

        private void Awake() => _renderer = GetComponent<SpriteRenderer>();

        private void Start() => _renderer.sprite = _weaponFacade.Weapon.PickUp;
        
        public void OnSpawned(Vector3 position, WeaponFacade weaponFacade, IMemoryPool pool)
        {
            _pool = pool;

            transform.position = position;
            _weaponFacade = weaponFacade;
        }

        public void Dispose() => _pool.Despawn(this);
        
        public void OnDespawned() => _pool = null;

        public class Factory : PlaceholderFactory<Vector3, WeaponFacade, WeaponGameObject>
        {
        }
    }
}