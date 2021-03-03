using System;
using UnityEngine;
using Zenject;

namespace Entities.Wave
{
    [RequireComponent(typeof(WaveMover))]
    public class WaveFacade : MonoBehaviour, IPoolable<Vector3, Vector2, Data.Wave, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        
        private WaveMover _waveMover;
        
        private float _timeToDestroy;
        private int _damageValue;

        public int DamageValue => _damageValue;

        private void Awake() => _waveMover = GetComponent<WaveMover>();

        private void Update()
        {
            if (_timeToDestroy <= 0)
                return;
            
            _timeToDestroy -= Time.deltaTime;
            if (_timeToDestroy <= 0)
                Dispose();
        }

        public void OnSpawned(Vector3 position, Vector2 direction, Data.Wave wave, IMemoryPool pool)
        {
            _pool = pool;
            
            transform.position = position;

            _waveMover.SetVelocity(wave.Velocity);
            _waveMover.SetDirection(direction);
            
            _timeToDestroy = wave.TimeToDestroy;
            _damageValue = wave.DamageValue;
        }

        public void Dispose() => _pool.Despawn(this);
        
        public void OnDespawned() => _pool = null;
        
        public class Factory : PlaceholderFactory<Vector3, Vector2, Data.Wave, WaveFacade>
        {
        }
    }
}