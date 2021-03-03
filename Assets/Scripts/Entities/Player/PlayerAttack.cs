using System;
using Entities.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Entities.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerAttack : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            [Header("Audio")]
            public Sound Attack;
        }

        private Settings _settings;

        private Transform _attackDirection;
        private bool _attackDirectionIsVisible;

        private WeaponAttack _weaponAttack;

        private Renderer _attackDirectionRenderer;

        private PlayerInput _input;
        private InputAction _attackAction;
        private InputAction _showAttackDirectionAction;
        
        [Inject]
        public void Construct(Settings settings, Transform attackDirection, WeaponAttack weaponAttack)
        {
            _settings = settings;
            _attackDirection = attackDirection;
            _attackDirectionRenderer = _attackDirection.GetComponent<Renderer>();
            _weaponAttack = weaponAttack;
            
            _settings.Attack.CreateAudioSource(gameObject);
        }

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            _attackAction = _input.actions.FindAction("Attack");
            _showAttackDirectionAction = _input.actions.FindAction("ShowAttackDirection");
        }

        private void OnEnable()
        {
            _attackAction.started += OnAttack;
            _showAttackDirectionAction.started += OnShowAttackDirection;
        }

        private void OnDisable()
        {
            _attackAction.started -= OnAttack;
            _showAttackDirectionAction.started -= OnShowAttackDirection;
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            _settings.Attack.PlayOneShot();
            _weaponAttack.Attack(transform.position, _attackDirection);
        }

        private void OnShowAttackDirection(InputAction.CallbackContext context)
        {
            _attackDirectionIsVisible = !_attackDirectionIsVisible;
            _attackDirectionRenderer.enabled = _attackDirectionIsVisible;
        }
    }
}