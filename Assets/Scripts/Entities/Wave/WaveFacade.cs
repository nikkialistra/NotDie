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

        private int _id;
        private int _damageValue;
        private bool _isPenetrable;

        public int Id => _id;

        public int DamageValue => _damageValue;

        public bool IsPenetrable => _isPenetrable;

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

            _id = waveSpecs.Id;
            _damageValue = waveSpecs.Damage;
            _isPenetrable = waveSpecs.isPenetrable;
            
            _waveMover.SetPosition(waveSpecs.Position);
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