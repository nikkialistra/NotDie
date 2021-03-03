using Core;
using Core.Animators;
using Entities.Items;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private UnitAnimator.Settings _unitAnimator;
        [SerializeField] private CameraFollow.Settings _cameraFollow;
        [SerializeField] private WeaponGameObject.Settings _weaponGameObject;

        public override void InstallBindings()
        {
            Container.BindInstance(_unitAnimator);
            Container.BindInstance(_cameraFollow);
            Container.BindInstance(_weaponGameObject);
        }
    }
}