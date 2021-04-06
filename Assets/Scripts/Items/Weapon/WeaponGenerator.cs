using System.Collections.Generic;
using Core.Room;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Items.Weapon
{
    [RequireComponent(typeof(RoomConfigurator))]
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] private List<Data.Weapon> _weapons;

        private RoomConfigurator _roomConfigurator;
        
        private WeaponSpawner _weaponSpawner;
        private WeaponGameObjectSpawner _weaponGameObjectSpawner;

        [Inject]
        public void Construct(WeaponSpawner weaponSpawner, WeaponGameObjectSpawner weaponGameObjectSpawner)
        {
            _weaponSpawner = weaponSpawner;
            _weaponGameObjectSpawner = weaponGameObjectSpawner;
        }

        private void Awake() => _roomConfigurator = GetComponent<RoomConfigurator>();

        private void Start()
        {
            foreach (var weapon in _weapons)
            {
                var weaponFacade = CreateWeaponFacade(weapon);

                CreateWeaponGameObject(weaponFacade);
            }
        }

        private WeaponFacade CreateWeaponFacade(Data.Weapon weapon)
        {
            var weaponSpecs = new WeaponSpecs()
            {
                Weapon = weapon,
                Durability = Random.Range(0.8f, 1f)
            };

            var weaponFacade = _weaponSpawner.Spawn(weaponSpecs);
            return weaponFacade;
        }

        private void CreateWeaponGameObject(WeaponFacade weaponFacade)
        {
            var polygonBounds = _roomConfigurator.PolygonFloorBounds;
            var roomBounds = _roomConfigurator.PolygonFloorBounds.bounds;

            var createPosition = new Vector3(
                Random.Range(roomBounds.min.x, roomBounds.max.x),
                Random.Range(roomBounds.min.y, roomBounds.max.y)
            );
            
            if (!polygonBounds.OverlapPoint(createPosition))
                createPosition = _roomConfigurator.PolygonFloorBorder.ClosestPoint(createPosition);

            _weaponGameObjectSpawner.Spawn(createPosition, weaponFacade);
        }
    }
}