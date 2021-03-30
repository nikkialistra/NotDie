using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Entities.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerMover _playerMover;
        private Transform _attackDirection;
        private bool _attackDirectionIsVisible;

        private WeaponAttack _weaponAttack;

        private Renderer _attackDirectionRenderer;

        private PlayerInput _input;
        private InputAction _attackAction;
        private InputAction _showAttackDirectionAction;

        [Inject]
        public void Construct(PlayerMover playerMover, Transform attackDirection, WeaponAttack weaponAttack)
        {
            _playerMover = playerMover;
            _attackDirection = attackDirection;
            _attackDirectionRenderer = _attackDirection.GetComponent<Renderer>();
            _weaponAttack = weaponAttack;
        }

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _attackAction = _input.actions.FindAction("Attack");
            _showAttackDirectionAction = _input.actions.FindAction("ShowAttackDirection");
        }

        private void Update()
        {
            var isAttack = _attackAction.ReadValue<float>();
            if (isAttack > 0)
                Attack();
        }

        private void OnEnable() => _showAttackDirectionAction.performed += OnShowAttackDirection;

        private void OnDisable() => _showAttackDirectionAction.started -= OnShowAttackDirection;

        private void Attack() => _weaponAttack.Attack(_playerMover.PositionCenter, _attackDirection.transform);

        private void OnShowAttackDirection(InputAction.CallbackContext context)
        {
            _attackDirectionIsVisible = !_attackDirectionIsVisible;
            _attackDirectionRenderer.enabled = _attackDirectionIsVisible;
        }
    }
}