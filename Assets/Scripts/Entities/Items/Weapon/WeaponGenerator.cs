using System.Collections.Generic;
using Core;
using Core.Room;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities.Items.Weapon
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

        private void Awake()
        {
            _roomConfigurator = GetComponent<RoomConfigurator>();
        }

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
                Durability = Random.Range(0.5f, 1f)
            };

            var weaponFacade = _weaponSpawner.Spawn(weaponSpecs);
            return weaponFacade;
        }

        private void CreateWeaponGameObject(WeaponFacade weaponFacade)
        {
            var bounds = _roomConfigurator.Collider.bounds;

            var spawnPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );

            spawnPosition = _roomConfigurator.Collider.ClosestPoint(spawnPosition);

            _weaponGameObjectSpawner.Spawn(spawnPosition, weaponFacade);
        }
    }
}