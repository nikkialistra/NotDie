using System;
using Entities.Player;
using UnityEngine;
using Zenject;

namespace Entities.Wave
{
    [RequireComponent(typeof(WaveMover))]
    [RequireComponent(typeof(Animator))]
    public class WaveFacade : MonoBehaviour, IPoolable<Vector3, Vector2, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        
        private WaveMover _waveMover;

        private Animator _animator;

        private int _damageValue;

        public int DamageValue => _damageValue;

        private void Awake()
        {
            _waveMover = GetComponent<WaveMover>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
                Dispose();
        }

        public void OnSpawned(Vector3 position, Vector2 direction, IMemoryPool pool)
        {
            _pool = pool;
            
            _waveMover.SetPosition(position);
            _waveMover.SetVelocity(100);
            _waveMover.SetDirection(direction);
            
        }

        public void Dispose() => _pool.Despawn(this);
        
        public void OnDespawned() => _pool = null;

        public class Factory : PlaceholderFactory<Vector3, Vector2, WaveFacade>
        {
        }
    }
}