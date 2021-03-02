using Core.Pool;
using UnityEngine;

namespace Entities.Wave
{
    [RequireComponent(typeof(WaveMover))]
    public class WaveFacade : MonoBehaviour, IGameObjectPooled<WaveFacade>
    {
        private WaveMover _waveMover;

        private float _timeToDestroy;
        private int _damageValue;

        public int DamageValue => _damageValue;

        public IPool<WaveFacade> Pool { get; set; }

        private void Awake()
        {
            _waveMover = GetComponent<WaveMover>();
        }

        private void Update()
        {
            if (_timeToDestroy <= 0)
                return;
            
            _timeToDestroy -= Time.deltaTime;
            if (_timeToDestroy <= 0)
                Disable();
        }

        public void Initialize(Vector3 position, Quaternion rotation, Vector2 direction, Data.Wave wave)
        {
            transform.position = position;
            transform.rotation = rotation;

            _waveMover.SetVelocity(wave.Velocity);
            _waveMover.SetDirection(direction);
            
            _timeToDestroy = wave.TimeToDestroy;

            _damageValue = wave.DamageValue;
            
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            Pool.ReturnToPool(this);
        }
    }
}