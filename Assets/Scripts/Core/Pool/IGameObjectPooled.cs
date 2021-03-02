namespace Core.Pool
{
    public interface IGameObjectPooled<T>
    { 
        IPool<T> Pool { get; set; }
    }
}