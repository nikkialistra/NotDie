using System;
using UnityEngine;
using Zenject;

namespace Core
{
    [RequireComponent(typeof(Camera))]
    public class CameraFollow : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public Vector2 FollowOffset;
            public float Speed;
        }

        private Settings _settings;
        
        private GameObject _followObject;
        private Rigidbody2D _rigidbody;

        private Vector2 _threshold;

        private Camera _camera;
        
        [Inject]
        public void Construct(Settings settings, GameObject followObject)
        {
            _settings = settings;
            _followObject = followObject;
        }

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

            var moveSpeed = _rigidbody.velocity.magnitude > _settings.Speed ? _rigidbody.velocity.magnitude : _settings.Speed;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }

        private Vector3 CalculateThreshold()
        {
            var aspect = _camera.pixelRect;
            var threshold = new Vector2(_camera.orthographicSize * aspect.width / aspect.height, _camera.orthographicSize);
            threshold.x -= _settings.FollowOffset.x;
            threshold.y -= _settings.FollowOffset.y;
            return threshold;
        }
    }
}