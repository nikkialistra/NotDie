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
            [Range(0.5f, 2)] 
            public float TimeToBlockControlMultiplier;

            [Header("Audio")]
            public Sound PlayerMoving;
        }

        public Action<Vector2> MovedByImpulse;

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
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            _input = GetComponent<PlayerInput>();
            _moveAction = _input.actions.FindAction("Move");
        }

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
        }

        private void OnImpulsed(float impulse, AnimationCurve impulseCurve, float timeToBlockControl)
        {
            _playerUnderControl = false;

            var impulseDirection = (_attackDirection.position - transform.position).normalized;
            MovedByImpulse?.Invoke(impulseDirection);

            StartCoroutine(UnderImpulse(impulse, impulseDirection, impulseCurve, timeToBlockControl));
        }

        private IEnumerator UnderImpulse(float impulse, Vector3 impulseDirection, AnimationCurve impulseCurve, float timeToBlockControl)
        {
            var timeUnderImpulse = 0f;
            var impulseAtThisMoment = 0f;
            var finalTimeToBlockControl = timeToBlockControl * _settings.TimeToBlockControlMultiplier;

            while (timeUnderImpulse < finalTimeToBlockControl)
            {
                impulseAtThisMoment = impulseCurve.Evaluate(timeUnderImpulse / finalTimeToBlockControl) * impulse;

                _rigidbody.velocity += impulseAtThisMoment * (Vector2) impulseDirection;

                timeUnderImpulse += Time.deltaTime;
                yield return null;
            }
            
            _playerUnderControl = true;
        }

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
    }
}
