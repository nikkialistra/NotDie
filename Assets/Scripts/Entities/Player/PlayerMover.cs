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
            [Range(0, 1)]
            public float IdleSpeed;
        }

        public Action Moving;
        public Action Idle;

        public Action MovingRight;
        public Action MovingLeft;
        
        public Action<Vector2> MovedByImpulse;

        private Settings _settings;

        private bool _playerUnderControl = true;
        
        private Transform _attackDirection;
        private Rigidbody2D _rigidbody;
        
        private WeaponAttack _weaponAttack;
        private PlayerAnimator _playerAnimation;

        private Vector2 _moveDirection;

        private PlayerInput _input;
        private InputAction _moveAction;

        [Inject]
        public void Construct(Settings settings, Transform attackDirection, WeaponAttack weaponAttack, PlayerAnimator playerAnimator)
        {
            _settings = settings;
            _attackDirection = attackDirection;
            _weaponAttack = weaponAttack;
            _playerAnimation = playerAnimator;
            
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
            UpdateMovindState();
            
            _moveDirection = _playerUnderControl ? _moveAction.ReadValue<Vector2>() : Vector2.zero;
        }

        private void FixedUpdate()
        {
            if (_moveDirection != Vector2.zero) 
                MovePlayer();
        }

        private void MovePlayer() => _rigidbody.velocity += _moveDirection * (_settings.Speed * Time.fixedDeltaTime);

        private void UpdateMovindState()
        {
            if (_rigidbody.velocity.magnitude > _settings.IdleSpeed)
                Moving?.Invoke();
            else
                Idle?.Invoke();
            
            if (_rigidbody.velocity.x > 0)
                MovingRight?.Invoke();
            
            if (_rigidbody.velocity.x < 0)
                MovingLeft?.Invoke();
        }

        private void OnImpulsed(float impulse, AnimationCurve curve, float time)
        {
            _playerUnderControl = false;
            
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
        }
    }
}
