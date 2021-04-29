using Entities.Player.Items;
using Things.Data;
using UI.Views;

namespace UI.Presenters
{
    public class InventoryPresenter
    {
        private readonly Inventory _inventory;
        private readonly InventoryView _inventoryView;

        public InventoryPresenter(Inventory inventory, InventoryView inventoryView)
        {
            _inventory = inventory;
            _inventoryView = inventoryView;
        }

        public void SetUp()
        {
            _inventory.ItemChanged += OnItemChanged;
        }

        private void OnItemChanged(int index, Item item)
        {
            _inventoryView.SetItem(index, item);
        }
    }
}