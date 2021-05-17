using Core;
using Core.Room;
using Core.Saving;
using Entities.Enemies.EnemyWave;
using Entities.Player;
using Entities.Player.Animation;
using Entities.Player.Combat;
using Entities.Player.Items;
using Entities.Wave;
using Things.Data;
using Things.Item;
using Things.Weapon;
using UI;
using UI.Views;
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

        [Header("Items")]
        [SerializeField] private GameObject _itemFacadePrefab;
        [SerializeField] private GameObject _itemPrefab;

        [Header("Room")]
        [SerializeField] private RoomConfigurator _roomConfigurator;
        
        [Header("Enemies")]
        [SerializeField] private GameObject _enemyWaveFacadePrefab;
        
        [Header("UI")] 
        [SerializeField] private UiManager _uiManager;
        [SerializeField] private HpView _hpView;
        [SerializeField] private TimerView _timerView;
        [SerializeField] private WeaponsView _weaponsView;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private GameOverView _gameOverView;

        [Header("Other")] 
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private MenuManager _menuManager;

        public override void InstallBindings()
        {
            BindPlayerMovement();

            Container.Bind<Hp>()
                .AsSingle();

            BindPlayerWeaponSystem();
            
            Container.Bind<Inventory>()
                .AsSingle();

            BindWaveSpawner();
            BindEnemyWaveSpawner();
            
            BindWeaponSpawner();
            BindWeaponGameObjectSpawner();
            
            BindItemSpawner();
            BindItemGameObjectSpawner();

            BindRoom();

            BindUI();

            Container.BindInstance(_gameSettings);
            Container.BindInstance(_menuManager);
        }

        private void BindPlayerMovement()
        {
            Container.BindInstance(_playerMover);
            Container.BindInstance(_playerAnimator);
            
            Container.BindInstance(_player)
                .WhenInjectedInto<CameraFollow>();
            Container.BindInstance(_player)
                .WhenInjectedInto<AttackDirection>();

            Container.BindInstance(_attackDirection)
                .WhenInjectedInto<PlayerMover>();
            Container.BindInstance(_attackDirection)
                .WithId("attackDirection")
                .WhenInjectedInto<PlayerAttack>();
            Container.BindInstance(_attackDirection)
                .WhenInjectedInto<PlayerAnimator>();
        }

        private void BindPlayerWeaponSystem()
        {
            Container.BindInstance(_hand)
                .WithId("hand")
                .WhenInjectedInto<Weapons>();
            Container.BindInstance(_crossedSlot)
                .WithId("crossedSlot")
                .WhenInjectedInto<Weapons>();
            Container.Bind<Weapons>()
                .AsSingle();

            Container.BindInstance(_weaponAttack);
            Container.BindInstance(_throwingWeapon);
            
            Container.BindInstance(_throwingArrow)
                .WithId("throwingArrow")
                .WhenInjectedInto<PlayerAttack>();
        }

        private void BindWaveSpawner()
        {
            Container.Bind<WaveSpawner>()
                .AsSingle();

            Container.BindFactory<WaveSpecs, WaveFacade, WaveFacade.Factory>()
                .FromPoolableMemoryPool<WaveSpecs, WaveFacade, WaveFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(_waveFacadePrefab)
                    .UnderTransformGroup("Waves"));
        }

        private void BindEnemyWaveSpawner()
        {
            Container.Bind<EnemyWaveSpawner>()
                .AsSingle();

            Container.BindFactory<EnemyWaveSpecs, EnemyWaveFacade, EnemyWaveFacade.Factory>()
                .FromPoolableMemoryPool<EnemyWaveSpecs, EnemyWaveFacade, EnemyWaveFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(_enemyWaveFacadePrefab)
                    .UnderTransformGroup("EnemyWaves"));
        }

        private void BindWeaponSpawner()
        {
            Container.Bind<WeaponSpawner>()
                .AsSingle();

            Container.BindFactory<WeaponSpecs, WeaponFacade, WeaponFacade.Factory>()
                .FromPoolableMemoryPool<WeaponSpecs, WeaponFacade, WeaponFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(_weaponFacadePrefab)
                    .UnderTransformGroup("Weapons"));
        }

        private void BindWeaponGameObjectSpawner()
        {
            Container.Bind<WeaponGameObjectSpawner>()
                .AsSingle();
            
            Container.BindFactory<Vector3, WeaponFacade, WeaponGameObject, WeaponGameObject.Factory>()
                .FromPoolableMemoryPool<Vector3, WeaponFacade, WeaponGameObject, WeaponGameObjectPool>(poolBinder => poolBinder
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_weaponPrefab)
                .UnderTransformGroup("WeaponPickups"));
        }

        private void BindItemSpawner()
        {
            Container.Bind<ItemSpawner>()
                .AsSingle();

            Container.BindFactory<Item, ItemFacade, ItemFacade.Factory>()
                .FromPoolableMemoryPool<Item, ItemFacade, ItemFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(_itemFacadePrefab)
                    .UnderTransformGroup("Items"));
        }

        private void BindItemGameObjectSpawner()
        {
            Container.Bind<ItemGameObjectSpawner>()
                .AsSingle();
            
            Container.BindFactory<Vector3, ItemFacade, ItemGameObject, ItemGameObject.Factory>()
                .FromPoolableMemoryPool<Vector3, ItemFacade, ItemGameObject, ItemGameObjectPool>(poolBinder => poolBinder
                    .WithInitialSize(5)
                    .FromComponentInNewPrefab(_itemPrefab)
                    .UnderTransformGroup("ItemPickups"));
        }

        private void BindRoom() => Container.BindInstance(_roomConfigurator);

        private void BindUI()
        {
            Container.BindInstance(_hpView);
            Container.BindInstance(_timerView);
            Container.BindInstance(_weaponsView);
            Container.BindInstance(_inventoryView);
            Container.BindInstance(_gameOverView);

            Container.BindInstance(_uiManager);
        }

        private class WaveFacadePool : MonoPoolableMemoryPool<WaveSpecs, IMemoryPool, WaveFacade>
        {
        }

        private class EnemyWaveFacadePool : MonoPoolableMemoryPool<EnemyWaveSpecs, IMemoryPool, EnemyWaveFacade>
        {
        }

        private class WeaponFacadePool : MonoPoolableMemoryPool<WeaponSpecs, IMemoryPool, WeaponFacade>
        {
        }

        private class WeaponGameObjectPool : MonoPoolableMemoryPool<Vector3, WeaponFacade, IMemoryPool, WeaponGameObject>
        {
        }

        private class ItemFacadePool : MonoPoolableMemoryPool<Item, IMemoryPool, ItemFacade>
        {
        }

        private class ItemGameObjectPool : MonoPoolableMemoryPool<Vector3, ItemFacade, IMemoryPool, ItemGameObject>
        {
        }
    }
}