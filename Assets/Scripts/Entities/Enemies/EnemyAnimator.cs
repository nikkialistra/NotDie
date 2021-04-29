using UnityEngine;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        public bool IsFlipped { get; private set; }
        
        private Animator _animator;

        private readonly int _run = Animator.StringToHash("run");
        private readonly int _attack = Animator.StringToHash("attack");
        private readonly int _stun = Animator.StringToHash("stun");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void UpdateDirection(Vector3 direction)
        {
            _animator.SetBool(_run, direction.magnitude > 0);
            
            if (direction.x > 0)
                Flip(false);
            
            if (direction.x < 0)
                Flip(true);
        }

        public void Stun()
        {
            _animator.SetBool(_run, false);
        }

        private void Flip(bool value)
        {
            IsFlipped = value;
            
            var scale = transform.localScale;
            scale.x = value ? - 1 : 1;
            transform.localScale = scale;
        }

        public void Attack()
        {
            _animator.SetTrigger(_attack);
        }
    }
}