using UnityEngine;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Dummy))]
    public class DummyAnimator : MonoBehaviour
    {
        private Dummy _dummy;
        
        private Rigidbody2D _rigidBody;
        private Animator _animator;

        private readonly int _movingSpeed = Animator.StringToHash("movingSpeed");

        private void Awake()
        {
            _dummy = GetComponent<Dummy>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _animator.SetFloat(_movingSpeed, _rigidBody.velocity.magnitude);
            
            if (_dummy.MoveDirection.x > 0)
                LookingRight();
            
            if (_dummy.MoveDirection.x < 0)
                LookingLeft();
        }

        private void LookingRight()
        {
            var scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
        
        private void LookingLeft()
        {
            var scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
    }
}