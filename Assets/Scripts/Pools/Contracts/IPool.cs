namespace Pools.Contracts
{
    public interface IPool<T>
    {
        void ReturnToPool(T objectToReturn);
    }
}