using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Entities.Player
{
    public class AttackDirection : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            [Range(0, 10)]
            public float AttackDirectionLength;
            [Range(0, 3)]
            public float ImpulseDirectionMultiplier;
            [Range(0, 0.5f)] 
            public float ButtonPressToleranceTime;
        }

        private Settings _settings;

        private float _attackDirectionX;
        private float _attackDirectionY;

        private float _attackDirectionXLastTakenTime;
        private float _attackDirectionYLastTakenTime;

        private GameObject _player;
        private Transform _playerTransform;
        private PlayerMover _playerMover;

        private PlayerInput _input;
        private InputAction _moveAction;

        [Inject]
        public void Construct(Settings settings, GameObject player, PlayerMover playerMover)
        {
            _settings = settings;
            _player = player;
            _playerTransform = player.transform;
            _playerMover = playerMover;

            _playerMover.MovedByImpulse += UpdatePosition;
        }

        private void Awake()
        {
            _input = _player.GetComponent<PlayerInput>();
            _moveAction = _input.actions.FindAction("Move");
        }

        private void Start()
        {
            var attackDirection = new Vector2(1, 0);

            UpdatePosition(attackDirection);
        }

        private void Update()
        {
            TakeDirectionValues();

            var attackDirection = ComputeAttackDirection();

            UpdatePosition(attackDirection);
        }

        private void TakeDirectionValues()
        {
            var moveDirection = _moveAction.ReadValue<Vector2>();

            moveDirection = ClampTo0Or1(moveDirection);

            if (moveDirection.x != 0)
            {
                _attackDirectionX = moveDirection.x;
                _attackDirectionXLastTakenTime = Time.time;
            }

            if (moveDirection.y != 0)
            {
                _attackDirectionY = moveDirection.y;
                _attackDirectionYLastTakenTime = Time.time;
            }
        }

        private Vector2 ComputeAttackDirection()
        {
            var attackDirection = Vector2.zero;

            if (_attackDirectionXLastTakenTime - _attackDirectionYLastTakenTime > _settings.ButtonPressToleranceTime)
            {
                attackDirection.x = _attackDirectionX;
                attackDirection.y = 0;
                return attackDirection;
            }
            
            if (_attackDirectionYLastTakenTime - _attackDirectionXLastTakenTime > _settings.ButtonPressToleranceTime)
            {
                attackDirection.x = 0;
                attackDirection.y = _attackDirectionY;
                return attackDirection;
            }
            
            attackDirection.x = _attackDirectionX;
            attackDirection.y = _attackDirectionY;
            return attackDirection;
        }

        private static Vector2 ClampTo0Or1(Vector2 moveDirection)
        {
            if (moveDirection.x > 0)
                moveDirection.x = 1;
            if (moveDirection.y > 0)
                moveDirection.y = 1;
            
            if (moveDirection.x < 0)
                moveDirection.x = -1;
            if (moveDirection.y < 0)
                moveDirection.y = -1;
            
            return moveDirection;
        }

        private void UpdatePosition(Vector2 attackDirection)
        {
            if (attackDirection == Vector2.zero)
                return;

            transform.position = _playerTransform.position + (Vector3) attackDirection * _settings.ImpulseDirectionMultiplier;
        }
    }
}