using System;
using System.Collections;
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
            [Range(0, 1)]
            public float IdleSpeed;
        }

        public Action<float> Moving;
        public Action Idle;

        public Action<Vector2> MovedByImpulse;

        public Action<bool> MovingIsBlocked;

        private Settings _settings;

        private bool _playerUnderControl = true;
        
        private Transform _attackDirection;
        private Rigidbody2D _rigidbody;
        
        private WeaponAttack _weaponAttack;

        private Vector2 _moveDirection;

        private PlayerInput _input;
        private InputAction _moveAction;

        [Inject]
        public void Construct(Settings settings, Transform attackDirection, WeaponAttack weaponAttack, PlayerAnimator playerAnimator)
        {
            _settings = settings;
            _attackDirection = attackDirection;
            _weaponAttack = weaponAttack;

            _weaponAttack.Impulsed += OnImpulsed;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.drag = _settings.Damping;
            
            _input = GetComponent<PlayerInput>();
            _moveAction = _input.actions.FindAction("Move");
        }

        private void Update()
        {
            if (_playerUnderControl)
            {
                UpdateMovingState();
                _moveDirection = _moveAction.ReadValue<Vector2>();
            }
            else
            {
                _moveDirection = Vector2.zero;
            }
        }

        private void FixedUpdate()
        {
            if (_moveDirection != Vector2.zero) 
                MovePlayer();
        }

        private void MovePlayer() => _rigidbody.velocity += _moveDirection * (_settings.Speed * Time.fixedDeltaTime);

        private void UpdateMovingState()
        {
            if (_rigidbody.velocity.magnitude > _settings.IdleSpeed)
                Moving?.Invoke(_rigidbody.velocity.magnitude);
            else
                Idle?.Invoke();
        }

        private void OnImpulsed(float impulse, AnimationCurve curve, float time)
        {
            _playerUnderControl = false;
            MovingIsBlocked?.Invoke(true);
            
            var impulseDirection = (_attackDirection.position - transform.position).normalized;
            MovedByImpulse?.Invoke(impulseDirection);
            
            StartCoroutine(UnderImpulse(impulse, impulseDirection, curve, time));
        }

        private IEnumerator UnderImpulse(float impulse, Vector3 impulseDirection, AnimationCurve impulseCurve, float time)
        {
            var timeUnderImpulse = 0f;
            var impulseAtThisMoment = 0f;

            while (timeUnderImpulse < time)
            {
                impulseAtThisMoment = impulseCurve.Evaluate(timeUnderImpulse / time) * impulse;

                _rigidbody.velocity += impulseAtThisMoment * (Vector2) impulseDirection;

                timeUnderImpulse += Time.deltaTime;
                yield return null;
            }
            
            _playerUnderControl = true;
            MovingIsBlocked?.Invoke(false);
        }
    }
}
