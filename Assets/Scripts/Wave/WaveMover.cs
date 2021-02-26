using System;
using Pools.Contracts;
using UnityEngine;

namespace Wave
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WaveMover : MonoBehaviour
    {
        private float _velocity;
        private Vector2 _direction;

        private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void SetVelocity(float velocity)
        {
            _velocity = velocity;
        }

        private void FixedUpdate()
        {
            _rigidBody.velocity = _direction * (_velocity * Time.fixedDeltaTime);
        }

        public void SetDirection(Vector2 direction)
        {
            if (direction == Vector2.zero) return;
            
            _direction = direction;
            
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}