using System;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerAnimator : MonoBehaviour
    {
        public bool IsFlipped;
        
        private WeaponAttack _weaponAttack;

        private Animator _animator;

        private PlayerMover _playerMover;
        private Transform _attackDirection;

        private readonly int _isMoving = Animator.StringToHash("isMoving");
        private readonly int _comboReset = Animator.StringToHash("comboReset");

        [Inject]
        public void Construct(PlayerMover playerMover, WeaponAttack weaponAttack, Transform attackDirection)
        {
            _playerMover = playerMover;
            _attackDirection = attackDirection;

            _playerMover.Moving += OnMoving;
            _playerMover.Idle += OnIdle;

            _weaponAttack = weaponAttack;

            _weaponAttack.Attacked += OnAttacked;
            _weaponAttack.ComboExit += OnComboExit;
            _weaponAttack.ComboExit += OnComboExit;
        }
        
        private void Awake() => _animator = GetComponent<Animator>();
        
        private void OnMoving() => _animator.SetBool(_isMoving, true);

        private void OnIdle() => _animator.SetBool(_isMoving, false);

        private void Update()
        {
            if (transform.position.x < _attackDirection.position.x)
                LookingRight();
            
            if (transform.position.x > _attackDirection.position.x)
                LookingLeft();
        }

        private void LookingRight()
        {
            var scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;

            IsFlipped = false;
        }
        
        private void LookingLeft()
        {
            var scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;

            IsFlipped = true;
        }

        private void OnAttacked(int trigger, AnimationClip clip) => _animator.SetTrigger(trigger);

        private void OnComboExit() => _animator.SetTrigger(_comboReset);
    }
}