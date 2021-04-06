using UnityEngine;

namespace Items.Weapon
{
    public class WeaponGameObjectSpawner
    {
        private WeaponGameObject.Factory _weaponGameObjectFactory;

        public WeaponGameObjectSpawner(WeaponGameObject.Factory weaponGameObjectFactory) => _weaponGameObjectFactory = weaponGameObjectFactory;

        public void Spawn(Vector3 position, WeaponFacade weaponFacade)
        {
            _weaponGameObjectFactory.Create(position, weaponFacade);
        }
    }
}