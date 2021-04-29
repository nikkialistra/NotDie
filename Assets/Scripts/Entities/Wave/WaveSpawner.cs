namespace Entities.Wave
{
    public class WaveSpawner
    {
        private readonly WaveFacade.Factory _waveFactory;

        public WaveSpawner(WaveFacade.Factory waveFactory)
        {
            _waveFactory = waveFactory;
        }

        public void Spawn(WaveSpecs waveSpecs)
        {
            _waveFactory.Create(waveSpecs);
        }
    }
}