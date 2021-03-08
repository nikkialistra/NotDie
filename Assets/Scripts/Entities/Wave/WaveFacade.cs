using System;
using Entities.Player;
using UnityEngine;
using Zenject;

namespace Entities.Wave
{
    [RequireComponent(typeof(WaveMover))]
    [RequireComponent(typeof(Animator))]
    public class WaveFacade : MonoBehaviour, IPoolable<WaveSpecs, IMemoryPool>, IDisposable
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

        public void OnSpawned(WaveSpecs waveSpecs, IMemoryPool pool)
        {
            _pool = pool;

            _damageValue = waveSpecs.Damage;
            
            _waveMover.SetPosition(waveSpecs.Transform.position);
            _waveMover.SetDirection(waveSpecs.Direction);
            
            _animator.SetTrigger(waveSpecs.WaveTriggerName);
        }

        public void Dispose() => _pool.Despawn(this);
        
        public void OnDespawned() => _pool = null;

        public class Factory : PlaceholderFactory<WaveSpecs, WaveFacade>
        {
        }
    }
}