using UnityEngine;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class DummyAnimator : MonoBehaviour
    {
        private Animator _animator;

        private readonly int _run = Animator.StringToHash("run");

        private void Awake() => _animator = GetComponent<Animator>();

        public void UpdateDirection(Vector3 direction)
        {
            _animator.SetBool(_run, direction.magnitude > 0);
            
            if (direction.x > 0)
                LookingRight();
            
            if (direction.x < 0)
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