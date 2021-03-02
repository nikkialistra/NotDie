using System;
using Entities.Data;
using Entities.Wave;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Entities.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class AttackHandler : MonoBehaviour
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
        
        private WaveSpawner _waveSpawner;
        
        private Renderer _placerRenderer;

        private PlayerInput _input;
        private InputAction _attackAction;
        private InputAction _showAttackDirectionAction;
        
        [Inject]
        public void Construct(Settings settings, Transform attackDirection, WaveSpawner waveSpawner)
        {
            _settings = settings;
            _attackDirection = attackDirection;
            _waveSpawner = waveSpawner;
        }

        private void Awake()
        {
            _placerRenderer = _attackDirection.GetComponent<Renderer>();
            
            _settings.Attack.CreateAudioSource(gameObject);
            
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
            _waveSpawner.SpawnWave(transform.position, _attackDirection);
        }

        private void OnShowAttackDirection(InputAction.CallbackContext context)
        {
            _attackDirectionIsVisible = !_attackDirectionIsVisible;
            _placerRenderer.enabled = _attackDirectionIsVisible;
        }
    }
}