using Entities.Player.Animation;
using Entities.Player.Combat;
using Entities.Player.Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player
{
    [RequireComponent(typeof(WeaponsHandler))]
    [RequireComponent(typeof(InventoryHandler))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class ItemTaker : MonoBehaviour
    {
        private WeaponsHandler _weaponsHandler;
        private InventoryHandler _inventoryHandler;
        private PlayerAnimator _playerAnimator;

        private PlayerInput _input;
        private InputAction _takeDropAction;

        private void Awake()
        {
            _weaponsHandler = GetComponent<WeaponsHandler>();
            _inventoryHandler = GetComponent<InventoryHandler>();
            _playerAnimator = GetComponent<PlayerAnimator>();

            _input = GetComponent<PlayerInput>();
            _takeDropAction = _input.actions.FindAction("TakeDropThrowing");
        }
        
        private void OnEnable()
        {
            _takeDropAction.canceled += OnTakeDrop;
        }

        private void OnDisable()
        {
            _takeDropAction.canceled -= OnTakeDrop;
        }

        private void OnTakeDrop(InputAction.CallbackContext context)
        {
            if (HoldingButton(context) || !_playerAnimator.IsCurrentAnimationWithTag("ActionReady"))
            {
                return;
            }

            if (_inventoryHandler.TryTakeItem())
            {
                return;
            }
            
            _weaponsHandler.TakeDropWeapon();
        }
        
        private static bool HoldingButton(InputAction.CallbackContext context)
        {
            return context.duration >= 0.3f;
        }
    }
}