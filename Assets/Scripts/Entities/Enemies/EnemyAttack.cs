using System;
using System.Collections;
using Entities.Enemies.EnemyWave;
using Entities.Enemies.Species;
using Entities.Player;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    [RequireComponent(typeof(EnemyMover))]
    public class EnemyAttack : MonoBehaviour
    {
        public Things.Data.EnemyWave EnemyWave;
        
        public float WaveDistance
        {
            get => _waveDistance;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(WaveDistance));
                }
                
                _waveDistance = value;
            }
        }
        public float Cooldown
        {
            get => _cooldown;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Cooldown));
                }
                
                _cooldown = value;
            }
        }

        private float _waveDistance;
        private float _cooldown;
        private float _nextAttackTime;
        
        private int _waveCounter;

        private EnemyWaveSpawner _enemyWaveSpawner;
        
        private Enemy _enemy;
        private EnemyAnimator _enemyAnimator;
        private EnemyMover _enemyMover;

        [Inject]
        public void Construct(EnemyWaveSpawner enemyWaveSpawner)
        {
            _enemyWaveSpawner = enemyWaveSpawner;
        }

        private void Awake()
        {
            _enemy = GetComponent<Enemy>();
            _enemyAnimator = GetComponent<EnemyAnimator>();
            _enemyMover = GetComponent<EnemyMover>();
        }

        public void TryAttack(PlayerMover player)
        {
            if (Time.time < _nextAttackTime)
            {
                return;
            }

            _enemyAnimator.Attack();

            var velocity = EnemyWave.Impulse * (player.PositionCenter - _enemy.PositionCenter).normalized;
            _enemyMover.AddVelocity(velocity, EnemyWave.ImpulseCurve, EnemyWave.Length);
            
            StartCoroutine(SpawnWaveAfterDelay(EnemyWave.WaveDelay,player));
            
            _nextAttackTime = Time.time + _cooldown;
        }

        private IEnumerator SpawnWaveAfterDelay(float delay, PlayerMover player)
        {
            yield return new WaitForSeconds(delay);
            
            var enemyWaveSpecs = new EnemyWaveSpecs
            {
                Enemy = _enemy,
                Id = _waveCounter++,
                Position = _enemy.PositionCenter,
                DirectionDistance = (player.PositionCenter - _enemy.PositionCenter).normalized * _waveDistance,
                Damage = EnemyWave.Damage,
                isPenetrable = EnemyWave.isPenetrable,
                Prefab = EnemyWave.WavePrefab,
            };
            
            _enemyWaveSpawner.Spawn(enemyWaveSpecs);
        }
    }
}