using UnityEngine;
using Zenject;

namespace Entities.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerAnimator : MonoBehaviour
    {
        public bool IsFlipped;
        
        private Weapons _weapons;

        private Animator _animator;

        private PlayerMover _playerMover;
        private Transform _attackDirection;

        private readonly int _movingSpeed = Animator.StringToHash("movingSpeed");
        private readonly int _comboMove = Animator.StringToHash("comboMove");

        private int[] _weaponsTakenHashes =
        {
            Animator.StringToHash("handTaken"),
            Animator.StringToHash("daggerTaken")
        };

        [Inject]
        public void Construct(PlayerMover playerMover, WeaponAttack weaponAttack, Transform attackDirection, Weapons weapons)
        {
            _playerMover = playerMover;
            _attackDirection = attackDirection;

            _playerMover.Moving += OnMoving;
            _playerMover.Idle += OnIdle;

            _weapons = weapons;

            _weapons.LeftWeaponIsActive += OnLeftWeaponActive;
            _weapons.RightWeaponIsActive += OnRightWeaponActive;
            _weapons.RightWeaponChanged += ActiveWeaponChange;
        }

        private void Awake() => _animator = GetComponent<Animator>();

        private void Update()
        {
            if (transform.position.x < _attackDirection.position.x)
                LookingRight();
            
            if (transform.position.x > _attackDirection.position.x)
                LookingLeft();
        }
        public bool IsCurrentAnimationWithTag(string tag) => _animator.GetCurrentAnimatorStateInfo(0).IsTag(tag);
        
        private void OnMoving(float speed) => _animator.SetFloat(_movingSpeed, speed);
        
        public void PlayAttackAnimation(int trigger) => _animator.SetTrigger(trigger);

        public void MoveInCombo() => _animator.SetTrigger(_comboMove);

        private void OnIdle() => _animator.SetFloat(_movingSpeed, 0);

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

        private void OnLeftWeaponActive()
        {
            ActiveWeaponChange();
            _weapons.LeftWeaponChanged += ActiveWeaponChange;
            _weapons.RightWeaponChanged -= ActiveWeaponChange;
        }

        private void OnRightWeaponActive()
        {
            ActiveWeaponChange();
            _weapons.LeftWeaponChanged -= ActiveWeaponChange;
            _weapons.RightWeaponChanged += ActiveWeaponChange;
        }

        private void ActiveWeaponChange()
        {
            foreach (var takenHash in _weaponsTakenHashes)
                _animator.SetBool(takenHash, false);

            _animator.SetBool(_weapons.ActiveWeapon.HashedTakenName, true);
        }
    }
}