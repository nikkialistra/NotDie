using System;
using Entities.Enemies.Species;
using UnityEngine;
using Zenject;

namespace Entities.Enemies.EnemyWave
{
    [RequireComponent(typeof(EnemyWaveMover))]
    public class EnemyWaveFacade : MonoBehaviour, IPoolable<EnemyWaveSpecs, IMemoryPool>, IDisposable
    {
        public Enemy Enemy => _enemy;
        public int Id => _id;
        public int DamageValue => _damageValue;
        public bool IsPenetrable => _isPenetrable;

        private Enemy _enemy;
        private int _id;
        private int _damageValue;
        private bool _isPenetrable;

        private IMemoryPool _pool;

        private EnemyWaveMover _enemyWaveMover;
        private Animator _animator;
        private static readonly int _wave = Animator.StringToHash("wave");

        private void Awake() => _enemyWaveMover = GetComponent<EnemyWaveMover>();

        private void Update()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                Destroy(_animator.gameObject);
                Dispose();
            }
        }

        public void OnSpawned(EnemyWaveSpecs enemyWaveSpecs, IMemoryPool pool)
        {
            _pool = pool;

            _enemy = enemyWaveSpecs.Enemy;
            _id = enemyWaveSpecs.Id;
            _damageValue = enemyWaveSpecs.Damage;
            _isPenetrable = enemyWaveSpecs.isPenetrable;

            _enemyWaveMover.SetEnemy(_enemy);
            _enemyWaveMover.SetDirection(enemyWaveSpecs.Position, enemyWaveSpecs.DirectionDistance);

            var wave = Instantiate(enemyWaveSpecs.Prefab, transform.position, transform.rotation, transform);

            _animator = wave.GetComponent<Animator>();
            _animator.SetTrigger(_wave);
        }

        public void Dispose() => _pool.Despawn(this);
        
        public void OnDespawned() => _pool = null;

        public class Factory : PlaceholderFactory<EnemyWaveSpecs, EnemyWaveFacade>
        {
        }
    }
}