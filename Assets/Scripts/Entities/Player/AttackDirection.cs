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
        }

        private Settings _settings;

        private GameObject _player;
        private Transform _playerTransform;
        private PlayerMover _playerMover;

        private const int ImpulseDirectionMultiplier = 5;

        private PlayerInput _input;
        private InputAction _moveAction;

        [Inject]
        public void Construct(Settings settings, GameObject player, PlayerMover playerMover)
        {
            _settings = settings;
            _player = player;
            _playerTransform = player.transform;
            _playerMover = playerMover;

            _playerMover.MovedByImpulse += OnMovedByImpulse;
        }

        private void Awake()
        {
            _input = _player.GetComponent<PlayerInput>();
            _moveAction = _input.actions.FindAction("Move");
        }

        private void Start() => SetToDefaultPosition();

        private void Update()
        {
            var attackDirection = _moveAction.ReadValue<Vector2>();
            if (attackDirection == Vector2.zero)
                return;

            transform.position = _playerTransform.position + (Vector3) attackDirection;
        }

        private void OnMovedByImpulse(Vector2 impulseDirection) => transform.position =
            _playerTransform.position + (Vector3) impulseDirection * ImpulseDirectionMultiplier;

        private void SetToDefaultPosition() => transform.position += new Vector3(_settings.AttackDirectionLength, 0);
    }
}