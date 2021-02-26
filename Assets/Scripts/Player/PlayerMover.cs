using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [Header("Movement settings")]
        [Range(0, 100)]
        [SerializeField] private float _speed;
        [Range(0, 100)]
        [SerializeField] private float _damping;
    
        [Header("Placing Settings")]
        [SerializeField] private Transform _placer;

        [Range(0, 10)]
        [SerializeField] private float _rayLength;
    
    
        private Rigidbody2D _rigidbody;
        private Vector3 _moveDirection;
        private Vector3 _lastVelocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            SetPlacerToDefaultPosition();
        }

        private void Update()
        {
            _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            SetDamping();
        }

        private void FixedUpdate()
        {
            if (_moveDirection != Vector3.zero)
                MovePlayer();
            if (_lastVelocity != Vector3.zero)
                MovePlacer();
        }

        private void SetPlacerToDefaultPosition()
        {
            _placer.position = transform.position + new Vector3(_rayLength, 0);
        }

        private void SetDamping()
        {
            if (_rigidbody.drag != _damping)
                _rigidbody.drag = _damping;
        }

        private void MovePlayer()
        {
            _rigidbody.velocity += (Vector2) _moveDirection * (_speed * Time.fixedDeltaTime);

            _lastVelocity = _rigidbody.velocity;
        }
    
        private void MovePlacer()
        {
            var lengthMultiplier = _rayLength / _lastVelocity.magnitude;
        
            _placer.position = transform.position;
            _placer.position += _lastVelocity * lengthMultiplier;
        }
    }
}
