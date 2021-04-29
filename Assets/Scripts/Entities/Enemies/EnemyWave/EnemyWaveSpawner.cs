namespace Entities.Enemies.EnemyWave
{
    public class EnemyWaveSpawner
    {
        private EnemyWaveFacade.Factory _waveFactory;

        public EnemyWaveSpawner(EnemyWaveFacade.Factory waveFactory)
        {
            _waveFactory = waveFactory;
        }

        public void Spawn(EnemyWaveSpecs enemyWaveSpecs)
        {
            _waveFactory.Create(enemyWaveSpecs);
        }
    }
}