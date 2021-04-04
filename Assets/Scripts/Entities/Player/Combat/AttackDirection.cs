using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Entities.Player.Combat
{
    public class AttackDirection : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
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
        private PlayerMover _playerMover;
        private Weapons _weapons;

        private Vector2 _attackDirection  = new Vector2(1, 0);

        private PlayerInput _input;
        private InputAction _moveAction;

        [Inject]
        public void Construct(Settings settings, GameObject player, PlayerMover playerMover, Weapons weapons)
        {
            _settings = settings;
            _player = player;
            _playerMover = playerMover;
            _weapons = weapons;
            
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
            if (_input.currentControlScheme != "Keyboard")
                Compute();
            else
                ComputeForKeyboard();
        }

        private void Compute()
        {
            var moveDirection = _moveAction.ReadValue<Vector2>();
            if (moveDirection != Vector2.zero)
                _attackDirection = moveDirection;
        }

        private void ComputeForKeyboard()
        {
            var moveDirection = _moveAction.ReadValue<Vector2>();
            if (moveDirection == Vector2.zero)
                return;
            
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
            
            transform.position =_playerMover.PositionCenter + (Vector3) _attackDirection.normalized * _weapons.ActiveWeapon.Weapon.DirectionMultiplier;
        }

        private void OnMovingIsBlocked(bool isBlocked) => _takeDirectionBlocked = isBlocked;
    }
}