using Core.Pool;
using UnityEngine;

namespace Entities.Wave
{
    public class WaveSpawner : ObjectPool<WaveFacade>
    {
        [SerializeField] private Data.Wave _wave;
        
        protected override void SetObjectPool(WaveFacade newObject) => newObject.Pool = this;

        public void SpawnWave(Vector3 playerPosition, Transform attackDirection)
        {
            var direction = (attackDirection.position - playerPosition).normalized;

            var wave = Get();
            wave.Initialize(attackDirection.transform.position, Quaternion.identity, direction, _wave);
        }
    }
}