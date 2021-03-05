using System;
using System.Collections.Generic;
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
        private readonly int _comboReset = Animator.StringToHash("comboReset");

        private List<int> _attackingTriggers = new List<int>();

        [Inject]
        public void Construct(Settings settings, WeaponAttack weaponAttack)
        {
            _settings = settings;
            _weaponAttack = weaponAttack;
            
            _attackingTriggers.Add(Animator.StringToHash("handAttackingOne"));
            _attackingTriggers.Add(Animator.StringToHash("handAttackingTwo"));
            _attackingTriggers.Add(Animator.StringToHash("handAttackingTwo"));

            _weaponAttack.Attacked += OnAttacked;
            _weaponAttack.ComboReset += OnComboReset;
            _weaponAttack.ComboExit += OnComboReset;
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

        private void OnAttacked(int waveNumber)
        {
            if (waveNumber > _attackingTriggers.Count)
                throw new ArgumentOutOfRangeException(nameof(waveNumber), "wave should have corresponding item in list of triggers");
            
            _animator.SetTrigger(_attackingTriggers[waveNumber]);
        }

        private void OnComboReset() => _animator.SetTrigger(_comboReset);

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