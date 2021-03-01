namespace Services.PoolContracts
{
    public interface IPool<T>
    {
        void ReturnToPool(T objectToReturn);
    }
}