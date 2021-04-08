using Entities.Player;
using Entities.Player.Combat;
using Entities.Player.Items;
using Things.Weapon;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "PlayerSettingsInstaller", menuName = "Installers/PlayerSettingsInstaller")]
    public class PlayerSettingsInstaller : ScriptableObjectInstaller<PlayerSettingsInstaller>
    {
        [SerializeField] private Hp.Settings _hp;
        [SerializeField] private PlayerMover.Settings _playerMover;
        [SerializeField] private WeaponsHandler.Settings _weaponsHandler;
        [SerializeField] private InventoryHandler.Settings _inventoryHandler;
        [SerializeField] private AttackDirection.Settings _attackDirection;
        [SerializeField] private ThrowingWeapon.Settings _thrownWeapon;

        public override void InstallBindings()
        {
            Container.BindInstance(_hp);
            Container.BindInstance(_playerMover);
            Container.BindInstance(_weaponsHandler);
            Container.BindInstance(_inventoryHandler);
            Container.BindInstance(_attackDirection);
            Container.BindInstance(_thrownWeapon);
        }
    }
}