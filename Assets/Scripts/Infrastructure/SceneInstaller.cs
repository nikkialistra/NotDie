using Core;
using Entities.Items.Weapon;
using Entities.Player;
using Entities.Player.Animation;
using Entities.Player.Combat;
using Entities.Wave;
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

        [Header("WeaponsHandler")]
        [SerializeField] private WeaponFacade _hand;
        [SerializeField] private GameObject _weaponFacadePrefab;
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private WeaponAttack _weaponAttack;

        [Header("Waves")]
        [SerializeField] private GameObject _waveFacadePrefab;

        public override void InstallBindings()
        {
            BindPlayerMovement();

            Container.Bind<Hp>().AsSingle();

            BindPlayerWeaponSystem();

            BindWaveSpawner();
            
            BindWeaponSpawner();
            
            BindWeaponGameObjectSpawner();
        }

        private void BindPlayerMovement()
        {
            Container.BindInstance(_playerMover);
            Container.BindInstance(_playerAnimator);
            
            Container.BindInstance(_player).WhenInjectedInto<CameraFollow>();
            Container.BindInstance(_player).WhenInjectedInto<AttackDirection>();

            Container.BindInstance(_attackDirection).WhenInjectedInto<PlayerMover>();
            Container.BindInstance(_attackDirection).WhenInjectedInto<PlayerAttack>();
            Container.BindInstance(_attackDirection).WhenInjectedInto<PlayerAnimator>();
        }

        private void BindPlayerWeaponSystem()
        {
            Container.BindInstance(_hand).WhenInjectedInto<Weapons>();
            Container.Bind<Weapons>().AsSingle();

            Container.BindInstance(_weaponAttack);
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