using UnityEngine;

namespace Entities.Wave
{
    public class WaveSpawner
    {
        private WaveFacade.Factory _waveFactory;

        public WaveSpawner(WaveFacade.Factory waveFactory) => _waveFactory = waveFactory;

        public void Spawn(Vector3 playerPosition, Transform attackDirection, int waveNumber)
        {
            var direction = (attackDirection.position - playerPosition).normalized;
            
            _waveFactory.Create(attackDirection.position, direction);
        }
    }
}