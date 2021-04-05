﻿using Entities.Items.Weapon;
using Entities.Player.Animation;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Entities.Player.Combat
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerAttack : MonoBehaviour
    {
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
        public void Construct([Inject(Id = "attackDirection")] Transform attackDirection, [Inject(Id = "throwingArrow")] Transform throwingArrow, ThrowingWeapon throwingWeapon)
        {
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
            TryAttackOrThrow();
            TryUpdateThrowing();
        }

        private void TryAttackOrThrow()
        {
            var isAttackOrThrow = _attackThrowAction.ReadValue<float>();
            if (isAttackOrThrow > 0)
            {
                if (_throwing)
                    Throw();
                else
                    Attack();
            }
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
            if (context.duration < 0.3f)
                return;

            if (!_weaponsHandler.AnyWeaponActive)
                return;

            _thrown = false;

            _playerMover.TakeAwayControl();
            _playerAnimator.StartThrowing();
            _throwingWeapon.StartThrowing(_weaponsHandler.ActiveWeapon);
            
            _throwing = true;
            _throwingArrowRenderer.enabled = true;
        }

        private void OnCancelThrowing(InputAction.CallbackContext context)
        {
            if (context.duration < 0.3f)
                return;

            _playerMover.ReturnControl();
            _playerAnimator.StopThrowing();
            _throwingWeapon.StopThrowing();
            
            _throwing = false;
            _throwingArrowRenderer.enabled = false;
        }

        private void Throw()
        {
            if (_thrown)
                return;
            
            var weapon = _weaponsHandler.TryTakeOffWeapon();

            _playerAnimator.Throw();
            _throwingWeapon.Throw(weapon, _playerMover.PositionCenter, _attackDirection.transform.position);
            
            _thrown = true;
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