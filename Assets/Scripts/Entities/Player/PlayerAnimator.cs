using System;
using System.Collections;
using System.Collections.Generic;
using Entities.Data;
using UnityEngine;
using Zenject;

namespace Entities.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerAnimator : MonoBehaviour
    {
        private WeaponAttack _weaponAttack;

        private Animator _animator;

        private PlayerMover _playerMover;

        private readonly int _isMoving = Animator.StringToHash("isMoving");
        private readonly int _comboReset = Animator.StringToHash("comboReset");

        [Inject]
        public void Construct(PlayerMover playerMover, WeaponAttack weaponAttack)
        {
            _playerMover = playerMover;

            _playerMover.Moving += OnMoving;
            _playerMover.Idle += OnIdle;

            _playerMover.MovingRight += OnMovingRight;
            _playerMover.MovingLeft += OnMovingLeft;

            _weaponAttack = weaponAttack;

            _weaponAttack.Attacked += OnAttacked;
            _weaponAttack.ComboExit += OnComboExit;
            _weaponAttack.ComboExit += OnComboExit;
        }
        
        private void Awake() => _animator = GetComponent<Animator>();
        
        private void OnMoving() => _animator.SetBool(_isMoving, true);

        private void OnIdle() => _animator.SetBool(_isMoving, false);
        
        private void OnMovingRight()
        {
            var scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
        
        private void OnMovingLeft()
        {
            var scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }

        private void OnAttacked(int trigger, AnimationClip clip) => _animator.SetTrigger(trigger);

        private void OnComboExit() => _animator.SetTrigger(_comboReset);
    }
}