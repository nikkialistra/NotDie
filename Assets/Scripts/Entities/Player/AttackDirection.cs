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

        private bool _takeDirectionBlocked;

        private GameObject _player;
        private Transform _playerTransform;
        private PlayerMover _playerMover;

        private Vector2 _attackDirection  = new Vector2(1, 0);

        private PlayerInput _input;
        private InputAction _moveAction;

        [Inject]
        public void Construct(Settings settings, GameObject player, PlayerMover playerMover)
        {
            _settings = settings;
            _player = player;
            _playerTransform = player.transform;
            _playerMover = playerMover;
            
            _playerMover.MovingIsBlocked += OnMovingIsBlocked;
        }

        private void Awake()
        {
            _input = _player.GetComponent<PlayerInput>();
            _moveAction = _input.actions.FindAction("Move");
        }

        private void Start() => UpdatePosition();

        private void Update()
        {
            if (!_takeDirectionBlocked)
                ComputeAttackDirection();

            UpdatePosition();
        }

        private void ComputeAttackDirection()
        {
            if (_input.currentControlScheme == "Keyboard")
                ComputeForKeyboard();
            else
                _attackDirection = _moveAction.ReadValue<Vector2>();
        }

        private void ComputeForKeyboard()
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

            ComputeKeyCashing();
        }

        private void ComputeKeyCashing()
        {
            _attackDirection = Vector2.zero;

            if (_attackDirectionXLastTakenTime - _attackDirectionYLastTakenTime > _settings.ButtonPressToleranceTime)
            {
                _attackDirection.x = _attackDirectionX;
                _attackDirection.y = 0;
                return;
            }
            
            if (_attackDirectionYLastTakenTime - _attackDirectionXLastTakenTime > _settings.ButtonPressToleranceTime)
            {
                _attackDirection.x = 0;
                _attackDirection.y = _attackDirectionY;
                return;
            }
            
            _attackDirection.x = _attackDirectionX;
            _attackDirection.y = _attackDirectionY;
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

        private void UpdatePosition()
        {
            if (_attackDirection == Vector2.zero)
                return;

            transform.position = _playerTransform.position + (Vector3) _attackDirection.normalized * _settings.ImpulseDirectionMultiplier;
        }

        private void OnMovingIsBlocked(bool isBlocked) => _takeDirectionBlocked = isBlocked;
    }
}