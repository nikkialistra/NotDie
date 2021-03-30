using Core;
using UI.Views;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private CameraFollow.Settings _cameraFollow;
        [SerializeField] private TimerView.Settings _timerView;
        [SerializeField] private WeaponsView.Settings _weaponsView;

        public override void InstallBindings()
        {
            Container.BindInstance(_cameraFollow);
            Container.BindInstance(_timerView);
            Container.BindInstance(_weaponsView);
        }
    }
}