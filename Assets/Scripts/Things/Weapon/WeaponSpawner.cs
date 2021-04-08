namespace Things.Weapon
{
    public class WeaponSpawner
    {
        private WeaponFacade.Factory _weaponFactory;

        public WeaponSpawner(WeaponFacade.Factory weaponFactory) => _weaponFactory = weaponFactory;

        public WeaponFacade Spawn(WeaponSpecs weaponSpecs)
        {
            var weaponFacade = _weaponFactory.Create(weaponSpecs);
            return weaponFacade;
        }
    }
}