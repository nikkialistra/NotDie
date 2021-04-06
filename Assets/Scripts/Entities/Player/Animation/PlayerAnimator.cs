using Entities.Player.Combat;
using UnityEngine;
using Zenject;

namespace Entities.Player.Animation
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        public bool IsFlipped { get; private set; }

        private Weapons _weapons;

        private Animator _animator;

        private Transform _attackDirection;

        private readonly int _run = Animator.StringToHash("run");
        private readonly int _comboMove = Animator.StringToHash("comboMove");
        private readonly int _throwing = Animator.StringToHash("throwing");
        private readonly int _cancelThrow = Animator.StringToHash("cancelThrow");
        private readonly int _throw = Animator.StringToHash("throw");


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
            if (_attackDirection.position.x - transform.position.x > 0)
                Flip(false);
            
            if (_attackDirection.position.x - transform.position.x < 0)
                Flip(true);
        }

        public void Run(bool shouldRun) => _animator.SetBool(_run, shouldRun);

        public bool IsCurrentAnimationWithTag(string tag) => _animator.GetCurrentAnimatorStateInfo(0).IsTag(tag);

        public void PlayAttackAnimation(int trigger) => _animator.SetTrigger(trigger);

        public void MoveInCombo() => _animator.SetTrigger(_comboMove);

        public void StartThrowing() => _animator.SetBool(_throwing, true);

        public void StopThrowing() => _animator.SetTrigger(_cancelThrow);

        public void Throw() => _animator.SetTrigger(_throw);

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
        
        private void Flip(bool value)
        {
            IsFlipped = value;
            
            var scale = transform.localScale;
            scale.x = value ? - 1 : 1;
            transform.localScale = scale;
        }
    }
}