using System;
using Entities.Data;
using Entities.Wave;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player
{
    public class AttackHandler : MonoBehaviour
    {
        [SerializeField] private Transform _attackDirection;
        
        [SerializeField] private WaveSpawner _waveSpawner;

        [Header("Audio")]
        [SerializeField] private Sound _attack;
        
        
        private bool _placerIsVisible;
        private Renderer _placerRenderer;
        
        private PlayerInput _input;
        private InputAction _attackAction;
        private InputAction _showAttackDirectionAction;

        private void Awake()
        {
            _placerRenderer = _attackDirection.GetComponent<Renderer>();
            
            _attack.CreateAudioSource(gameObject);
            
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
            _attack.MultiplePlay();
            _waveSpawner.SpawnWave(transform.position, _attackDirection);
        }

        private void OnShowAttackDirection(InputAction.CallbackContext context)
        {
            _placerIsVisible = !_placerIsVisible;
            _placerRenderer.enabled = _placerIsVisible;
        }
    }
}