using System;
using Entities.Player.Animation;
using Things.Item;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Entities.Player.Items
{
    public class InventoryHandler : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public float DistanceForTaking;
        }

        private Settings _settings;
        
        private Inventory _inventory;

        private PlayerAnimator _playerAnimator;

        private PlayerInput _input;
        private InputAction _takeAction;

        [Inject]
        public void Construct(Settings settings, Inventory inventory)
        {
            _settings = settings;
            _inventory = inventory;
        }

        private void Awake()
        {
            _playerAnimator = GetComponent<PlayerAnimator>();
            _input = GetComponent<PlayerInput>();
            _takeAction = _input.actions.FindAction("TakeDropThrowingWeapon");
        }

        private void OnEnable() => _takeAction.canceled += OnTakeItem;

        private void OnDisable() => _takeAction.canceled -= OnTakeItem;

        private void OnTakeItem(InputAction.CallbackContext context)
        {
            if (HoldingButton(context))
                return;
            
            if (!_playerAnimator.IsCurrentAnimationWithTag("ActionReady"))
                return;

            TryTakeItem();
        }

        private static bool HoldingButton(InputAction.CallbackContext context)
        {
            return context.duration > 0.3f;
        }

        private void TryTakeItem()
        {
            var items = FindObjectsOfType<ItemGameObject>();
            foreach (var item in items)
            {
                if (Vector3.Distance(transform.position, item.transform.position) >
                    _settings.DistanceForTaking) continue;
                
                TakeItem(item);
            }
        }
        
        private void TakeItem(ItemGameObject itemGameObject)
        {
            var itemFacade = itemGameObject.ItemFacade;
            
            if (_inventory.TryTakeItem(itemFacade))
                itemGameObject.Dispose();
        }
    }
}