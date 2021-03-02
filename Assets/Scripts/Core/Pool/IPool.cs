namespace Core.Pool
{
    public interface IPool<T>
    {
        void ReturnToPool(T objectToReturn);
    }
}