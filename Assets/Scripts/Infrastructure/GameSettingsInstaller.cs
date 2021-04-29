using Core;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private CameraFollow.Settings _cameraFollow;

        public override void InstallBindings()
        {
            Container.BindInstance(_cameraFollow);
        }
    }
}