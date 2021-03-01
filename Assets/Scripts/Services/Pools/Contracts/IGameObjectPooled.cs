namespace Services.Pools.Contracts
{
    public interface IGameObjectPooled<T>
    { 
        IPool<T> Pool { get; set; }
    }
}