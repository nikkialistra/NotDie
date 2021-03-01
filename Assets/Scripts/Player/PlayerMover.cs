using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Movement settings")]
        [Range(0, 100)]
        [SerializeField] private float _speed;
        [Range(0, 100)]
        [SerializeField] private float _damping;
    
        [Header("Direction Settings")]
        [SerializeField] private Transform _attackDirection;

        [Range(0, 10)]
        [SerializeField] private float _attackDirectionLength;

        private Rigidbody2D _rigidbody;
        private Vector2 _moveDirection;
        private Vector2 _lastVelocity;
        
        private PlayerInput _input;
        private InputAction _moveAction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            _input = GetComponent<PlayerInput>();
            _moveAction = _input.actions.FindAction("Move");
        }

        private void Start() => SetAttackDirectionToDefaultPosition();

        private void Update()
        {
            _moveDirection = _moveAction.ReadValue<Vector2>();
            SetDamping();
        }

        private void FixedUpdate()
        {
            if (_moveDirection != Vector2.zero)
                MovePlayer();
            if (_lastVelocity != Vector2.zero)
                MoveAttackDirection();
        }

        private void SetAttackDirectionToDefaultPosition()
        {
            _attackDirection.position = transform.position + new Vector3(_attackDirectionLength, 0);
        }

        private void SetDamping()
        {
            if (_rigidbody.drag != _damping)
                _rigidbody.drag = _damping;
        }

        private void MovePlayer()
        {
            _rigidbody.velocity += _moveDirection * (_speed * Time.fixedDeltaTime);

            _lastVelocity = _rigidbody.velocity;
        }
    
        private void MoveAttackDirection()
        {
            var lengthMultiplier = _attackDirectionLength / _lastVelocity.magnitude;
        
            _attackDirection.position = transform.position;
            _attackDirection.position += (Vector3) _lastVelocity * lengthMultiplier;
        }
    }
}
