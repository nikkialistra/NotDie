using UnityEngine;

namespace Entities.Wave
{
    public class WaveSpawner
    {
        private WaveFacade.Factory _waveFactory;
        private Data.Wave _wave;
        
        public WaveSpawner(WaveFacade.Factory waveFactory, Data.Wave wave)
        {
            _waveFactory = waveFactory;
            _wave = wave;
        }

        public void SpawnWave(Vector3 playerPosition, Transform attackDirection)
        {
            var direction = (attackDirection.position - playerPosition).normalized;

            var wave = _waveFactory.Create(attackDirection.transform.position, direction, _wave);
        }
    }
}