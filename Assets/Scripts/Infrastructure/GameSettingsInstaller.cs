using Core;
using Entities.Player;
using UI.Views;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private PlayerAnimator.Settings _unitAnimator;
        [SerializeField] private CameraFollow.Settings _cameraFollow;
        [SerializeField] private TimerView.Settings _timerView;
        [SerializeField] private WeaponAttack.Settings _weaponAttack;

        public override void InstallBindings()
        {
            Container.BindInstance(_unitAnimator);
            Container.BindInstance(_cameraFollow);
            Container.BindInstance(_timerView);
            Container.BindInstance(_weaponAttack);
        }
    }
}