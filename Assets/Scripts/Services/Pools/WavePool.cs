using Services.Pools.Contracts;
using Wave;

namespace Services.Pools
{
    public class WavePool : ObjectPool<WaveFacade>
    {
        protected override void SetObjectPool(WaveFacade newObject)
        {
            newObject.Pool = this;
        }
    }
}