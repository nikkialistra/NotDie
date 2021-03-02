using UnityEngine;

namespace Core.Animators
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class UnitAnimator : MonoBehaviour
    {
        [SerializeField] private float _idleSpeed;
    
    
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

        private void SetMovingAnimation()
        {
            _animator.SetBool(_isMoving, _rigidbody.velocity.magnitude > _idleSpeed);
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