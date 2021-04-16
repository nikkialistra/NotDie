using System;
using System.Collections;
using Entities.Player.Animation;
using Things.Weapon;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Entities.Player.Combat
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerAttack : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            [Range(0, 1f)] 
            public float AttackAfterThrowCooldown;
        }

        private Settings _settings;
        
        private PlayerMover _playerMover;
        private PlayerAnimator _playerAnimator;
        
        private ThrowingWeapon _throwingWeapon;

        private Transform _attackDirection;
        private Renderer _throwingArrowRenderer;

        private bool _attackDirectionIsVisible;

        private Transform _throwingArrow;
        private bool _throwing;
        private bool _thrown;

        private WeaponsHandler _weaponsHandler;
        private WeaponAttack _weaponAttack;

        private Renderer _attackDirectionRenderer;

        private PlayerInput _input;
        private InputAction _attackThrowAction;
        private InputAction _showAttackDirectionAction;
        private InputAction _throwingAction;

        [Inject]
        public void Construct(Settings settings, [Inject(Id = "attackDirection")] Transform attackDirection, [Inject(Id = "throwingArrow")] Transform throwingArrow, ThrowingWeapon throwingWeapon)
        {
            _settings = settings;
            
            _attackDirection = attackDirection;
            _attackDirectionRenderer = _attackDirection.GetComponent<Renderer>();

            _throwingArrow = throwingArrow;
            _throwingArrowRenderer = _throwingArrow.GetComponent<Renderer>();

            _throwingWeapon = throwingWeapon;
        }

        private void Awake()
        {
            _playerMover = GetComponent<PlayerMover>();
            _weaponsHandler = GetComponent<WeaponsHandler>();
            _weaponAttack = GetComponent<WeaponAttack>();
            _playerAnimator = GetComponent<PlayerAnimator>();

            _input = GetComponent<PlayerInput>();
            _attackThrowAction = _input.actions.FindAction("AttackThrow");
            _showAttackDirectionAction = _input.actions.FindAction("ShowAttackDirection");
            _throwingAction = _input.actions.FindAction("TakeDropThrowingWeapon");
        }

        private void Update()
        {
            if (_attackThrowAction.ReadValue<float>() > 0)
                ThrowOrAttack();
            else
                TryUpdateThrowing();
        }

        private void ThrowOrAttack()
        {
            if (_throwing)
                Throw();
            else
                Attack();
        }
        
        private void TryUpdateThrowing()
        {
            if (_throwing)
                Throwing();
        }

        private void Throwing()
        {
            _throwingArrow.transform.position = _playerMover.PositionCenter;
            var direction = (_attackDirection.position - _playerMover.PositionCenter).normalized;
            
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _throwingArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void OnEnable()
        {
            _showAttackDirectionAction.started += OnShowAttackDirection;
            _throwingAction.performed += OnThrowing;
            _throwingAction.canceled += OnCancelThrowing;
        }

        private void OnDisable()
        {
            _showAttackDirectionAction.started -= OnShowAttackDirection;
            _throwingAction.performed -= OnThrowing;
            _throwingAction.canceled -= OnCancelThrowing;
        }

        private void Attack() => _weaponAttack.Attack(_playerMover.PositionCenter, _attackDirection.transform);

        private void OnThrowing(InputAction.CallbackContext context)
        {
            if (NotHoldingButton(context))
                return;

            if (!_weaponsHandler.WeaponHeld || _playerAnimator.IsCurrentAnimationWithTag("Shot"))
                return;

            _thrown = false;

            _playerAnimator.StartThrowing();
            _throwingWeapon.StartThrowing(_weaponsHandler.ActiveWeapon);
            
            _throwing = true;
            _throwingArrowRenderer.enabled = true;
            
            _playerMover.TakeAwayControl();
        }

        private void OnCancelThrowing(InputAction.CallbackContext context)
        {
            if (NotHoldingButton(context))
                CancelThrowing();
        }

        private static bool NotHoldingButton(InputAction.CallbackContext context) => context.duration < 0.3f;

        private void Throw()
        {
            if (_thrown || !_playerAnimator.IsCurrentAnimationWithTag("Transition"))
                return;

            if (!_weaponsHandler.WeaponHeld)
            {
                CancelThrowing();
                return;
            }

            var weapon = _weaponsHandler.TakeOffWeapon();

            _playerAnimator.Throw();
            _throwingWeapon.Throw(weapon, _playerMover.PositionCenter, _attackDirection.transform.position);
            
            _thrown = true;
            _throwingArrowRenderer.enabled = false;
            
            _playerMover.ReturnControl();

            StartCoroutine(ReturnAttackControl());
        }

        private IEnumerator ReturnAttackControl()
        {
            yield return new WaitForSeconds(_settings.AttackAfterThrowCooldown);
            _throwing = false;
        }

        private void CancelThrowing()
        {
            if (_thrown)
                return;

            _playerAnimator.StopThrowing();
            _throwingWeapon.StopThrowing();

            _throwing = false;
            _throwingArrowRenderer.enabled = false;

            _playerMover.ReturnControl();
        }

        private void OnShowAttackDirection(InputAction.CallbackContext context)
        {
            _attackDirectionIsVisible = !_attackDirectionIsVisible;
            _attackDirectionRenderer.enabled = _attackDirectionIsVisible;
        }
    }
}