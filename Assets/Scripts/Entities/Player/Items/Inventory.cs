using System;
using Things.Data;
using Things.Item;

namespace Entities.Player.Items
{
    public class Inventory
    {
        public event Action<int, Item> ItemChanged;
        
        private ItemFacade[] _items = new ItemFacade[5];

        public bool TryTakeItem(ItemFacade itemFacade)
        {
            if (itemFacade == null)
                throw new ArgumentNullException(nameof(itemFacade));

            var index = Array.FindIndex(_items, item => item == null);
            
            if (index == -1)
                return false;

            _items[index] = itemFacade;

            ItemChanged?.Invoke(index, itemFacade.Item);

            return true;
        }
    }
}