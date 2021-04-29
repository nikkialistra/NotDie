using System.Collections.Generic;
using Core.Room;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Things.Item
{
    [RequireComponent(typeof(RoomConfigurator))]
    public class ItemGenerator : MonoBehaviour
    {
        [SerializeField] private List<Data.Item> _items;

        private RoomConfigurator _roomConfigurator;
        
        private ItemSpawner _itemSpawner;
        private ItemGameObjectSpawner _itemGameObjectSpawner;

        [Inject]
        public void Construct(ItemSpawner itemSpawner, ItemGameObjectSpawner itemGameObjectSpawner)
        {
            _itemSpawner = itemSpawner;
            _itemGameObjectSpawner = itemGameObjectSpawner;
        }

        private void Awake()
        {
            _roomConfigurator = GetComponent<RoomConfigurator>();
        }

        private void Start()
        {
            foreach (var item in _items)
            {
                var itemFacade = CreateItemFacade(item);

                CreateItemGameObject(itemFacade);
            }
        }

        private ItemFacade CreateItemFacade(Data.Item item)
        {
            var itemFacade = _itemSpawner.Spawn(item);
            return itemFacade;
        }

        private void CreateItemGameObject(ItemFacade itemFacade)
        {
            var polygonBounds = _roomConfigurator.PolygonFloorBounds;
            var roomBounds = _roomConfigurator.PolygonFloorBounds.bounds;

            var createPosition = new Vector3(
                Random.Range(roomBounds.min.x, roomBounds.max.x),
                Random.Range(roomBounds.min.y, roomBounds.max.y)
            );

            if (!polygonBounds.OverlapPoint(createPosition))
            {
                createPosition = _roomConfigurator.PolygonFloorBorder.ClosestPoint(createPosition);
            }

            _itemGameObjectSpawner.Spawn(createPosition, itemFacade);
        }
    }
}