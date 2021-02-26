using Pools.Contracts;
using Wave;

namespace Pools
{
    public class WavePool : ObjectPool<WaveFacade>
    {
        protected override void SetObjectPool(WaveFacade newObject)
        {
            newObject.Pool = this;
        }
    }
}