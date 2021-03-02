using Entities.Wave;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform _attackDirection;
        [SerializeField] private WaveSpawner _waveSpawner;

        public override void InstallBindings()
        {
            Container.BindInstance(_attackDirection).AsSingle();
            Container.BindInstance(_waveSpawner).AsSingle();
        }
    }
}