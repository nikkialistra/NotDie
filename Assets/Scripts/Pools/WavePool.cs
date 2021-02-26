using Pools.Contracts;
using Wave;

namespace Pools
{
    public class WavePool : ObjectPool<WaveMover>
    {
        protected override void SetObjectPool(WaveMover newObject)
        {
            newObject.Pool = this;
        }
    }
}