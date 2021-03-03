using Entities.Enemies;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "EnemiesSettingsInstaller", menuName = "Installers/EnemiesSettingsInstaller")]
    public class EnemiesSettingsInstaller : ScriptableObjectInstaller<EnemiesSettingsInstaller>
    {
        [Header("Enemies")]
        [SerializeField] private Dummy.Settings _dummy;
        [SerializeField] private EnemyHealthHandler.Settings _enemyHealthHandler;
    
        public override void InstallBindings()
        {
            Container.BindInstance(_dummy);
            Container.BindInstance(_enemyHealthHandler);
        }
    }
}