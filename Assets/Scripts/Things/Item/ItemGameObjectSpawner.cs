using UnityEngine;

namespace Things.Item
{
    public class ItemGameObjectSpawner
    {
        private ItemGameObject.Factory _itemGameObjectFactory;

        public ItemGameObjectSpawner(ItemGameObject.Factory itemGameObjectFactory) => _itemGameObjectFactory = itemGameObjectFactory;

        public void Spawn(Vector3 position, ItemFacade itemFacade)
        {
            _itemGameObjectFactory.Create(position, itemFacade);
        }
    }
}