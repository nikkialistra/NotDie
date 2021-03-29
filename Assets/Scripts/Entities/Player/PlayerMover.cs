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

        public Action<bool> MovingIsBlocked;

        private Settings _settings;

        private bool _playerUnderControl = true;
        
        private Transform _attackDirection;
        private Rigidbody2D _rigidbody;

        private Vector2 _moveDirection;

        private PlayerInput _input;
        private InputAction _moveAction;

        [Inject]
        public void Construct(Settings settings, Transform attackDirection)
        {
            _settings = settings;
            _attackDirection = attackDirection;
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

       public void AddImpulse(float impulse, AnimationCurve curve, float time)
        {
            _playerUnderControl = false;
            MovingIsBlocked?.Invoke(true);
            
            var impulseDirection = (_attackDirection.position - transform.position).normalized;

            StartCoroutine(UnderImpulse(impulse, impulseDirection, curve, time));
        }

        private IEnumerator UnderImpulse(float impulse, Vector3 impulseDirection, AnimationCurve impulseCurve, float time)
        {
            var timeUnderImpulse = 0f;

            while (timeUnderImpulse < time)
            {
                var impulseAtThisMoment = impulseCurve.Evaluate(timeUnderImpulse / time) * impulse;

                _rigidbody.velocity += impulseAtThisMoment * (Vector2) impulseDirection;

                timeUnderImpulse += Time.deltaTime;
                yield return null;
            }
            
            _playerUnderControl = true;
            MovingIsBlocked?.Invoke(false);
        }
    }
}
