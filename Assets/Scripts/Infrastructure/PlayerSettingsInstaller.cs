using Entities.Player;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "PlayerSettingsInstaller", menuName = "Installers/PlayerSettingsInstaller")]
    public class PlayerSettingsInstaller : ScriptableObjectInstaller<PlayerSettingsInstaller>
    {
        [SerializeField] private AttackHandler.Settings _attackHandler;
        [SerializeField] private Hp.Settings _hp;
        [SerializeField] private HpHandler.Settings _hpHandler;
        [SerializeField] private PlayerMover.Settings _playerMover;
        [SerializeField] private WeaponHandler.Settings _weaponHandler;

        public override void InstallBindings()
        {
            Container.BindInstance(_attackHandler);
            Container.BindInstance(_hp);
            Container.BindInstance(_hpHandler);
            Container.BindInstance(_playerMover);
            Container.BindInstance(_weaponHandler);
        }
    }
}