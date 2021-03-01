using UnityEngine;

namespace Services
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private GameObject _followObject;
        [SerializeField] private Vector2 _followOffset;
    
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private Vector2 _threshold;
        private Camera _camera;

        private void Awake()
        {
            _rigidbody = _followObject.GetComponent<Rigidbody2D>();
            _camera = GetComponent<Camera>();
        }

        private void Start()
        {
            _threshold = CalculateThreshold();
        }

        private void FixedUpdate()
        {
            Vector2 follow = _followObject.transform.position;
            var xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
            var yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

            var newPosition = transform.position;
            if (Mathf.Abs(xDifference) >= _threshold.x)
                newPosition.x = follow.x;
        
            if (Mathf.Abs(yDifference) >= _threshold.y)
                newPosition.y = follow.y;

            var moveSpeed = _rigidbody.velocity.magnitude > _speed ? _rigidbody.velocity.magnitude : _speed;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }
    
        private Vector3 CalculateThreshold()
        {
            var aspect = _camera.pixelRect;
            var threshold = new Vector2(_camera.orthographicSize * aspect.width / aspect.height, _camera.orthographicSize);
            threshold.x -= _followOffset.x;
            threshold.y -= _followOffset.y;
            return threshold;
        }
    }
}