namespace Services.PoolContracts
{
    public interface IGameObjectPooled<T>
    { 
        IPool<T> Pool { get; set; }
    }
}