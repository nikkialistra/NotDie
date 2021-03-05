using System;
using System.Collections;
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
            [Range(0, 2)] 
            public float TimeToReturnControl;

            [Header("Direction Settings")]
            [Range(0, 10)]
            public float AttackDirectionLength;

            [Header("Audio")]
            public Sound PlayerMoving;
        }

        private Settings _settings;

        private bool _playerUnderControl = true;
        
        private Transform _attackDirection;
        private Rigidbody2D _rigidbody;
        
        private WeaponAttack _weaponAttack;

        private Vector2 _moveDirection;
        private Vector2 _lastVelocity;

        private PlayerInput _input;
        private InputAction _moveAction;

        [Inject]
        public void Construct(Settings settings, Transform attackDirection, WeaponAttack weaponAttack)
        {
            _settings = settings;
            _attackDirection = attackDirection;
            _weaponAttack = weaponAttack;

            _weaponAttack.Impulsed += OnImpulsed;
            
            _settings.PlayerMoving.CreateAudioSource(gameObject);
        }

        private void OnImpulsed(float impulse, AnimationCurve impulseCurve)
        {
            _playerUnderControl = false;

            var impulseDirection = (_attackDirection.position - transform.position).normalized;

            StartCoroutine(UnderImpulse(impulse, impulseDirection, impulseCurve));
        }

        private IEnumerator UnderImpulse(float impulse, Vector3 impulseDirection, AnimationCurve impulseCurve)
        {
            var timeUnderImpulse = 0f;
            var impulseAtThisMoment = 0f;

            while (timeUnderImpulse < _settings.TimeToReturnControl)
            {
                impulseAtThisMoment = impulseCurve.Evaluate(timeUnderImpulse / _settings.TimeToReturnControl) * impulse;

                _rigidbody.velocity += impulseAtThisMoment * (Vector2) impulseDirection;

                timeUnderImpulse += Time.deltaTime;
                yield return null;
            }
            
            _playerUnderControl = true;
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
            _moveDirection = _playerUnderControl ? _moveAction.ReadValue<Vector2>() : Vector2.zero;

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
