using Services.PoolContracts;

namespace Entities.Wave
{
    public class WavePool : ObjectPool<WaveFacade>
    {
        protected override void SetObjectPool(WaveFacade newObject)
        {
            newObject.Pool = this;
        }
    }
}