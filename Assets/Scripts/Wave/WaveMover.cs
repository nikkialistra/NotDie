using Pools.Contracts;
using UnityEngine;

namespace Wave
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WaveMover : MonoBehaviour, IGameObjectPooled<WaveMover>
    {
        [SerializeField] private float _velocity;

        private Rigidbody2D _rigidBody;

        private float _timeToDestroy;

        public IPool<WaveMover> Pool { get; set; }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_timeToDestroy <= 0)
                return;
            
            _timeToDestroy -= Time.deltaTime;
            if (_timeToDestroy <= 0)
                Pool.ReturnToPool(this);
        }

        public void SetDirection(Vector2 direction)
        {
            _rigidBody.velocity = direction * (_velocity * Time.fixedDeltaTime);

            if (direction == Vector2.zero) return;
            
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public void SetTimeToDestroy(float timeToDestroy)
        {
            _timeToDestroy = timeToDestroy;
        }
    }
}