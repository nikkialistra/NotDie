using System;
using UnityEngine;
using Zenject;

namespace Things.Item
{
    public class ItemFacade : MonoBehaviour, IPoolable<Data.Item, IMemoryPool>, IDisposable
    {
        [SerializeField] private Data.Item _item;
        
        public Data.Item Item => _item;
        
        private IMemoryPool _pool;

        public void OnSpawned(Data.Item item, IMemoryPool pool)
        {
            _pool = pool;
            _item = item;
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public class Factory : PlaceholderFactory<Data.Item, ItemFacade>
        {
        }
    }
}