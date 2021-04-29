using System;
using UnityEngine;
using Zenject;

namespace Things.Item
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ItemGameObject : MonoBehaviour, IPoolable<Vector3, ItemFacade, IMemoryPool>, IDisposable
    {
        public ItemFacade ItemFacade => _itemFacade;

        private ItemFacade _itemFacade;

        private SpriteRenderer _renderer;

        private IMemoryPool _pool;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void OnSpawned(Vector3 position, ItemFacade itemFacade, IMemoryPool pool)
        {
            _pool = pool;
            transform.position = position;

            _itemFacade = itemFacade;
            _renderer.sprite = _itemFacade.Item.PickUp;
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public class Factory : PlaceholderFactory<Vector3, ItemFacade, ItemGameObject>
        {
        }
    }
}