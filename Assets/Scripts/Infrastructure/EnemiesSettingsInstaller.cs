using Entities.Enemies.Species;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "EnemiesSettingsInstaller", menuName = "Installers/EnemiesSettingsInstaller")]
    public class EnemiesSettingsInstaller : ScriptableObjectInstaller<EnemiesSettingsInstaller>
    {
        [SerializeField] private Dummy.Settings _dummy;
        [SerializeField] private Poor.Settings _poor;

        public override void InstallBindings()
        {
            Container.BindInstance(_dummy);
            Container.BindInstance(_poor);
        }
    }
}