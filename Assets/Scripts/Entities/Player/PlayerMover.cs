using System;
using Entities.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMover : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            [Header("Movement settings")]
            [Range(0, 100)]
            public float Speed;
            [Range(0, 100)]
            public float Damping;
    
            [Header("Direction Settings")]
            [Range(0, 10)]
            public float AttackDirectionLength;

            [Header("Audio")]
            public Sound PlayerMoving;
        }

        private Settings _settings;
        
        private Transform _attackDirection;

        private Rigidbody2D _rigidbody;

        private Vector2 _moveDirection;
        private Vector2 _lastVelocity;

        private PlayerInput _input;
        private InputAction _moveAction;
        
        [Inject]
        public void Construct(Settings settings, Transform attackDirection)
        {
            _settings = settings;
            _attackDirection = attackDirection;
            
            _settings.PlayerMoving.CreateAudioSource(gameObject);
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            _input = GetComponent<PlayerInput>();
            _moveAction = _input.actions.FindAction("Move");
        }

        private void Start() => SetAttackDirectionToDefaultPosition();

        private void Update()
        {
            _moveDirection = _moveAction.ReadValue<Vector2>();
            SetDamping();
        }

        private void FixedUpdate()
        {
            if (_moveDirection != Vector2.zero)
            {
                _settings.PlayerMoving.Play();
                MovePlayer();
            }
            else
            {
                _settings.PlayerMoving.Stop();
            }

            if (_lastVelocity != Vector2.zero)
                MoveAttackDirection();
        }

        private void SetAttackDirectionToDefaultPosition() => _attackDirection.position = transform.position + new Vector3(_settings.AttackDirectionLength, 0);

        private void SetDamping()
        {
            if (_rigidbody.drag != _settings.Damping)
                _rigidbody.drag = _settings.Damping;
        }

        private void MovePlayer()
        {
            _rigidbody.velocity += _moveDirection * (_settings.Speed * Time.fixedDeltaTime);
            
            _lastVelocity = _rigidbody.velocity;
        }

        private void MoveAttackDirection()
        {
            var lengthMultiplier = _settings.AttackDirectionLength / _lastVelocity.magnitude;
        
            _attackDirection.position = transform.position;
            _attackDirection.position += (Vector3) _lastVelocity * lengthMultiplier;
        }
    }
}
