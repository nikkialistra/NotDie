using Entities.Player.Combat;
using UnityEngine;
using Zenject;

namespace Entities.Player.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        public bool IsFlipped;
        
        private Weapons _weapons;

        private Animator _animator;
        
        private Transform _attackDirection;

        private readonly int _run = Animator.StringToHash("run");
        private readonly int _comboMove = Animator.StringToHash("comboMove");

        private readonly int[] _weaponsTakenHashes =
        {
            Animator.StringToHash("handTaken"),
            Animator.StringToHash("daggerTaken")
        };

        [Inject]
        public void Construct(Transform attackDirection, Weapons weapons)
        {
            _attackDirection = attackDirection;

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

        public void Run(bool shouldRun) => _animator.SetBool(_run, shouldRun);
        
        public bool IsCurrentAnimationWithTag(string tag) => _animator.GetCurrentAnimatorStateInfo(0).IsTag(tag);
        
        
        public void PlayAttackAnimation(int trigger) => _animator.SetTrigger(trigger);

        public void MoveInCombo() => _animator.SetTrigger(_comboMove);

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

            _animator.SetBool(_weapons.ActiveWeapon.Weapon.HashedTakenName, true);
        }
    }
}