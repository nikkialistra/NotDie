using System;
using Core;
using Entities.Data;
using Entities.Items;
using Entities.Player;
using Entities.Wave;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _attackDirection;
        
        [SerializeField] private Weapon _hand;

        [SerializeField] private GameObject _weapon;

        [SerializeField] private GameObject _waveFacadePrefab;
        [SerializeField] private Wave _wave;

        public override void InstallBindings()
        {
            BindPlayerMovement();

            Container.Bind<Hp>().AsSingle();
            
            BindPlayerWeaponSystem();
            
            Container.BindFactory<Weapon, Vector3, WeaponGameObject, WeaponGameObject.Factory>()
                .FromComponentInNewPrefab(_weapon)
                .UnderTransformGroup("Weapons");

            BindWaveSpawner();
        }

        private void BindPlayerMovement()
        {
            Container.BindInstance(_player).WhenInjectedInto<CameraFollow>();

            Container.BindInstance(_attackDirection).WhenInjectedInto<PlayerMover>();
            Container.BindInstance(_attackDirection).WhenInjectedInto<AttackHandler>();
        }

        private void BindPlayerWeaponSystem()
        {
            Container.BindInstance(_hand).WhenInjectedInto<Weapons>();
            Container.Bind<Weapons>().AsSingle();
        }

        private void BindWaveSpawner()
        {
            Container.BindInstance(_wave);
            Container.Bind<WaveSpawner>().AsSingle();

            Container.BindFactory<Vector3, Vector2, Wave, WaveFacade, WaveFacade.Factory>()
                .FromPoolableMemoryPool<Vector3, Vector2, Wave, WaveFacade, WaveFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(10)
                    .FromComponentInNewPrefab(_waveFacadePrefab)
                    .UnderTransformGroup("Waves"));
        }

        class WaveFacadePool : MonoPoolableMemoryPool<Vector3, Vector2, Wave, IMemoryPool, WaveFacade>
        {
        }
    }
}