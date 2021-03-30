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
            [Range(0, 1)]
            public float YPosition;
            [Header("Movement settings")]
            [Range(0, 100)]
            public float Speed;
            [Range(0, 100)]
            public float Damping;
        }

        public Action<bool> MovingIsBlocked;

        private Settings _settings;

        private bool _playerUnderControl = true;
        
        private Transform _attackDirection;
        private Rigidbody2D _rigidbody;

        private WeaponAttack _weaponAttack;

        private PlayerAnimator _playerAnimator;
        
        private Vector2 _moveDirection;

        private PlayerInput _input;
        private InputAction _moveAction;

        [Inject]
        public void Construct(Settings settings, Transform attackDirection, WeaponAttack weaponAttack, PlayerAnimator playerAnimator)
        {
            _settings = settings;
            _attackDirection = attackDirection;
            _weaponAttack = weaponAttack;
            _playerAnimator = playerAnimator;
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
                _moveDirection = _moveAction.ReadValue<Vector2>();
            else
                _moveDirection = Vector2.zero;
        }

        public Vector3 PositionCenter => transform.position + new Vector3(0, _settings.YPosition);

        private void FixedUpdate()
        {
            if (_moveDirection != Vector2.zero) 
                MovePlayer();
            else
                _playerAnimator.Run(false);
        }

        public void AddImpulse(float impulse, AnimationCurve curve, float time)
        {
            _playerUnderControl = false;
            MovingIsBlocked?.Invoke(true);
            
            var impulseDirection = (_attackDirection.position - PositionCenter).normalized;

            StartCoroutine(UnderImpulse(impulse, impulseDirection, curve, time));
        }

        private void MovePlayer()
        {
            _playerAnimator.Run(true);
            _weaponAttack.TryMoveInCombo();
            _rigidbody.velocity += _moveDirection * (_settings.Speed * Time.fixedDeltaTime);
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
