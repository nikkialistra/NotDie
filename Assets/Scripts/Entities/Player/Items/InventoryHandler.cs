using System;
using Things.Data;
using Things.Item;
using UnityEngine;
using Zenject;

namespace Entities.Player.Items
{
    public class InventoryHandler : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public float DistanceForTaking;
        }

        public event Action<ItemFacade> InventoryChange; 

        private Settings _settings;
        
        private Inventory _inventory;

        [Inject]
        public void Construct(Settings settings, Inventory inventory)
        {
            _settings = settings;
            _inventory = inventory;
        }

        public bool TryTakeItem()
        {
            var items = FindObjectsOfType<ItemGameObject>();
            foreach (var item in items)
            {
                if (Vector3.Distance(transform.position, item.transform.position) >
                    _settings.DistanceForTaking)
                {
                    continue;
                }
                
                TakeItem(item);
                return true;
            }

            return false;
        }
        
        private void TakeItem(ItemGameObject itemGameObject)
        {
            var itemFacade = itemGameObject.ItemFacade;

            if (_inventory.TryTakeItem(itemFacade))
            {
                InventoryChange?.Invoke(itemFacade);
                itemGameObject.Dispose();
            }
        }
    }
}