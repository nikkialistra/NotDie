using Entities.Data;
using Entities.RoomItems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private Weapons _weapons;
        
        [SerializeField] private GameObject _weaponPrefab;

        [SerializeField] private float _distanceForTaking;

        private PlayerInput _input;
        private InputAction _takeDropWeaponAction;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _takeDropWeaponAction = _input.actions.FindAction("TakeDropWeapon");
        }

        private void OnEnable() => _takeDropWeaponAction.started += OnTakeDropWeapon;

        private void OnDisable() => _takeDropWeaponAction.started -= OnTakeDropWeapon;

        private void OnTakeDropWeapon(InputAction.CallbackContext context)
        {
            if (TryTakeWeapon()) 
                return;

            DropWeapon();
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