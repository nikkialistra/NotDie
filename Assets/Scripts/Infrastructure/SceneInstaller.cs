using Core;
using Core.Room;
using Entities.Player;
using Entities.Player.Animation;
using Entities.Player.Combat;
using Entities.Wave;
using Items.Weapon;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class SceneInstaller : MonoInstaller
    {
        [Header("Player base")]
        [SerializeField] private GameObject _player;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Transform _attackDirection;
        [SerializeField] private Transform _throwingArrow;
        [SerializeField] private ThrowingWeapon _throwingWeapon;

        [Header("Weapons")]
        [SerializeField] private WeaponFacade _hand;
        [SerializeField] private WeaponFacade _crossedSlot;
        [SerializeField] private GameObject _weaponFacadePrefab;
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private WeaponAttack _weaponAttack;
        [SerializeField] private GameObject _waveFacadePrefab;

        [Header("Room")]
        [SerializeField] private RoomConfigurator _roomConfigurator;
        [SerializeField] private PolygonCollider2D _polygonFloorBounds;
        [SerializeField] private PolygonCollider2D _polygonWallBounds;
        [SerializeField] private EdgeCollider2D _polygonFloorBorder;
        [SerializeField] private EdgeCollider2D _polygonWallBorder;

        public override void InstallBindings()
        {
            BindPlayerMovement();

            Container.Bind<Hp>().AsSingle();

            BindPlayerWeaponSystem();

            BindWaveSpawner();
            
            BindWeaponSpawner();
            
            BindWeaponGameObjectSpawner();

            BindRoom();
        }

        private void BindPlayerMovement()
        {
            Container.BindInstance(_playerMover);
            Container.BindInstance(_playerAnimator);
            
            Container.BindInstance(_player).WhenInjectedInto<CameraFollow>();
            Container.BindInstance(_player).WhenInjectedInto<AttackDirection>();

            Container.BindInstance(_attackDirection).WhenInjectedInto<PlayerMover>();
            Container.BindInstance(_attackDirection).WithId("attackDirection").WhenInjectedInto<PlayerAttack>();
            Container.BindInstance(_attackDirection).WhenInjectedInto<PlayerAnimator>();
        }

        private void BindPlayerWeaponSystem()
        {
            Container.BindInstance(_hand).WithId("hand").WhenInjectedInto<Weapons>();
            Container.BindInstance(_crossedSlot).WithId("crossedSlot").WhenInjectedInto<Weapons>();
            Container.Bind<Weapons>().AsSingle();

            Container.BindInstance(_weaponAttack);
            Container.BindInstance(_throwingWeapon);
            
            Container.BindInstance(_throwingArrow).WithId("throwingArrow").WhenInjectedInto<PlayerAttack>();
        }

        private void BindWaveSpawner()
        {
            Container.Bind<WaveSpawner>().AsSingle();

            Container.BindFactory<WaveSpecs, WaveFacade, WaveFacade.Factory>()
                .FromPoolableMemoryPool<WaveSpecs, WaveFacade, WaveFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(_waveFacadePrefab)
                    .UnderTransformGroup("Waves"));
        }

        private void BindWeaponSpawner()
        {
            Container.Bind<WeaponSpawner>().AsSingle();

            Container.BindFactory<WeaponSpecs, WeaponFacade, WeaponFacade.Factory>()
                .FromPoolableMemoryPool<WeaponSpecs, WeaponFacade, WeaponFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(_weaponFacadePrefab)
                    .UnderTransformGroup("Weapons"));
        }

        private void BindWeaponGameObjectSpawner()
        {
            Container.Bind<WeaponGameObjectSpawner>().AsSingle();
            
            Container.BindFactory<Vector3, WeaponFacade, WeaponGameObject, WeaponGameObject.Factory>()
                .FromPoolableMemoryPool<Vector3, WeaponFacade, WeaponGameObject, WeaponGameObjectPool>(poolBinder => poolBinder
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_weaponPrefab)
                .UnderTransformGroup("WeaponPickups"));
        }

        private void BindRoom()
        {
            Container.BindInstance(_roomConfigurator);
            
            Container.BindInstance(_polygonFloorBounds).WithId("floor");
            Container.BindInstance(_polygonWallBounds).WithId("wall");
            Container.BindInstance(_polygonFloorBorder).WithId("floorBorder");
            Container.BindInstance(_polygonWallBorder).WithId("wallBorder");
        }

        class WaveFacadePool : MonoPoolableMemoryPool<WaveSpecs, IMemoryPool, WaveFacade>
        {
        }

        class WeaponFacadePool : MonoPoolableMemoryPool<WeaponSpecs, IMemoryPool, WeaponFacade>
        {
        }
        
        class WeaponGameObjectPool : MonoPoolableMemoryPool<Vector3, WeaponFacade, IMemoryPool, WeaponGameObject>
        {
        }
    }
}