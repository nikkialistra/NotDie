using System;
using Things.Weapon;
using UnityEngine;
using Zenject;

namespace Entities.Wave
{
    [RequireComponent(typeof(WaveMover))]
    public class WaveFacade : MonoBehaviour, IPoolable<WaveSpecs, IMemoryPool>, IDisposable
    {
        public int Id => _id;
        public int DamageValue => _damageValue;
        public bool IsPenetrable => _isPenetrable;

        public int ReclineValue => _reclineValue;

        private int _id;
        private WeaponFacade _weaponFacade;
        private int _damageValue;
        private bool _isPenetrable;
        private int _reclineValue;

        private IMemoryPool _pool;

        private WaveMover _waveMover;
        private Animator _animator;

        private void Awake() => _waveMover = GetComponent<WaveMover>();

        private void Update()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                Destroy(_animator.gameObject);
                Dispose();
            }
        }

        public void Hitted() => _weaponFacade.Hitted();

        public void OnSpawned(WaveSpecs waveSpecs, IMemoryPool pool)
        {
            _pool = pool;

            _id = waveSpecs.Id;
            _weaponFacade = waveSpecs.WeaponFacade;
            _damageValue = waveSpecs.Damage;
            _isPenetrable = waveSpecs.isPenetrable;
            _reclineValue = waveSpecs.ReclineValue;
            
            _waveMover.SetPosition(waveSpecs.Transform.position);
            _waveMover.SetDirection(waveSpecs.Direction);

            var wave = Instantiate(waveSpecs.Prefab, transform.position, transform.rotation, transform);

            _animator = wave.GetComponent<Animator>();
            _animator.SetTrigger(waveSpecs.WaveTriggerName);
        }

        public void Dispose() => _pool.Despawn(this);
        
        public void OnDespawned() => _pool = null;

        public class Factory : PlaceholderFactory<WaveSpecs, WaveFacade>
        {
        }
    }
}