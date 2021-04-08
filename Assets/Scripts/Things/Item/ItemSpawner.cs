namespace Things.Item
{
    public class ItemSpawner
    {
        private ItemFacade.Factory _itemFactory;

        public ItemSpawner(ItemFacade.Factory itemFactory) => _itemFactory = itemFactory;

        public ItemFacade Spawn(Data.Item item)
        {
            var itemFacade = _itemFactory.Create(item);
            return itemFacade;
        }
    }
}