﻿using System;
using Entities.Player;
using UnityEngine;
using Zenject;

namespace Entities.Wave
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WaveMover : MonoBehaviour
    {
        private float _velocity;
        private Vector2 _direction;

        private PlayerAnimator _playerAnimator;
        private PlayerMover _playerMover;

        private Rigidbody2D _playerRigidbody;

        private Rigidbody2D _rigidBody;

        [Inject]
        public void Construct(PlayerAnimator playerAnimator, PlayerMover playerMover)
        {
            _playerAnimator = playerAnimator;
            _playerMover = playerMover;
        }

        private void Awake() => _rigidBody = GetComponent<Rigidbody2D>();

        public void SetVelocity(float velocity)
        {
            if (velocity < 0)
                throw new ArgumentException("Velocity should be positive");
            
            _velocity = velocity;
        }

        private void FixedUpdate() => _rigidBody.velocity = _playerRigidbody.velocity;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;

            _playerRigidbody = _playerMover.GetComponent<Rigidbody2D>();
        }
        
        public void SetDirection(Vector2 direction)
        {
            if (direction == Vector2.zero) 
                return;

            int additionalRotation;

            if (_playerAnimator.IsFlipped)
            {
                Flip();
                additionalRotation = 180;
            }
            else
            {
                transform.localScale = Vector3.one;
                additionalRotation = 0;
            }

            _direction = direction;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + additionalRotation, Vector3.forward);
        }
        
        private void Flip()
        {
            var scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
    }
}