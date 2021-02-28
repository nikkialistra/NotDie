using Data;
using Items;
using UnityEngine;

namespace Player
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private Weapons _weapons;
        
        [SerializeField] private GameObject _weaponPrefab;

        [SerializeField] private float _distanceForTaking;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (TryTakeWeapon()) 
                    return;

                DropWeapon();
            }
        }

        private bool TryTakeWeapon()
        {
            var weapons = FindObjectsOfType<WeaponGameObject>();
            foreach (var weapon in weapons)
            {
                if (Vector3.Distance(transform.position, weapon.transform.position) < _distanceForTaking)
                {
                    TakeWeapon(weapon);
                    return true;
                }
            }

            return false;
        }

        private void DropWeapon()
        {
            var weapon = _weapons.DropWeapon();
            if (weapon != null)
                InstantiateWeapon(weapon);
        }

        private void TakeWeapon(WeaponGameObject weaponGameObject)
        {
            var discardedWeapon = _weapons.TakeWeapon(weaponGameObject.Weapon);
            Destroy(weaponGameObject.gameObject);

            if (discardedWeapon != null)
            {
                InstantiateWeapon(discardedWeapon);
            }
        }

        private void InstantiateWeapon(Weapon weaponToCreate)
        {
            var weapon = Instantiate(_weaponPrefab, transform.position, Quaternion.identity);

            var weaponGameObject = weapon.GetComponent<WeaponGameObject>();
            weaponGameObject.SetWeapon(weaponToCreate);
        }
    }
}