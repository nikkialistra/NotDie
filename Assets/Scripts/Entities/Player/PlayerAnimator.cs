using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Entities.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerAnimator : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public float IdleSpeed;
        }

        private Settings _settings;

        private WeaponAttack _weaponAttack;

        private Animator _animator;

        private Rigidbody2D _rigidbody;

        private readonly int _isMoving = Animator.StringToHash("isMoving");
        
        private readonly int _handAttackingOne = Animator.StringToHash("handAttackingOne");
        private readonly int _handAttackingTwo = Animator.StringToHash("handAttackingTwo");

        [Inject]
        public void Construct(Settings settings, WeaponAttack weaponAttack)
        {
            _settings = settings;
            _weaponAttack = weaponAttack;

            _weaponAttack.Attacking += OnAttacking;
        }

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

        private void OnAttacking(int waveNumber)
        {
            _animator.SetTrigger(waveNumber == 0 ? _handAttackingOne : _handAttackingTwo);
        }

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