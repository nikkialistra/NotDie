using System;
using UnityEngine;

namespace Core.Animators
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitAnimator : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public float IdleSpeed;
        }

        private Settings _settings;

        private Animator _animator;

        private Rigidbody2D _rigidbody;

        private readonly int _isMoving = Animator.StringToHash("isMoving");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            SetMovingAnimation();
            Flip();
        }

        private void SetMovingAnimation() => _animator.SetBool(_isMoving, _rigidbody.velocity.magnitude > _settings.IdleSpeed);

        private void Flip()
        {
            if (_rigidbody.velocity.x == 0)
                return;
        
            var scale = transform.localScale;

            if (_rigidbody.velocity.x > 0)
                scale.x = 1;
            else
                scale.x = -1;
        
            transform.localScale = scale;
        }
    }
}