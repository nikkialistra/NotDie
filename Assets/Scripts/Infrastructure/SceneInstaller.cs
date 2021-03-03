using Core;
using Entities.Data;
using Entities.Player;
using Entities.Wave;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _player;

        [SerializeField] private Weapon _hand;
        
        [SerializeField] private Transform _attackDirection;
        [SerializeField] private WaveSpawner _waveSpawner;

        public override void InstallBindings()
        {
            BindPlayerMovement();

            Container.Bind<Hp>().AsSingle();
            
            BindPlayerWeaponSystem();
        }

        private void BindPlayerMovement()
        {
            Container.BindInstance(_player).WhenInjectedInto<CameraFollow>();

            Container.BindInstance(_attackDirection).WhenInjectedInto<PlayerMover>();
            Container.BindInstance(_attackDirection).WhenInjectedInto<AttackHandler>();
        }

        private void BindPlayerWeaponSystem()
        {
            Container.BindInstance(_hand);
            Container.Bind<Weapons>().AsSingle();
            Container.BindInstance(_waveSpawner).AsSingle();
        }
    }
}