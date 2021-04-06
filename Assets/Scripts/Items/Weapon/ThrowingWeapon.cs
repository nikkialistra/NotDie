using System;
using System.Collections;
using Core.Interfaces;
using Core.Room;
using UnityEngine;
using Zenject;

namespace Items.Weapon
{
    public class ThrowingWeapon : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            [Range(0, 1)]
            public float TimeToFly;
            [Range(0, 0.5f)]
            public float Speed;
            [Space]
            [Range(0, 1)]
            public float TimeToDrop;
            [Range(0, 0.5f)]
            public float DropSpeed;
        }
        
        private Settings _settings;
        private RoomConfigurator _roomConfigurator;

        private Rigidbody2D _rigidBody;

        private SpriteRenderer _spriteRenderer;
        private Animation _animation;

        private WeaponGameObjectSpawner _weaponGameObjectSpawner;

        private WeaponFacade _thrownWeapon;

        private Coroutine _fly;
        private Collider2D _collider;

        private void Start()
        {
            _collider = GetComponent<Collider2D>();
        }

        [Inject]
        public void Construct(Settings settings, RoomConfigurator roomConfigurator, WeaponGameObjectSpawner weaponGameObjectSpawner)
        {
            _settings = settings;
            _roomConfigurator = roomConfigurator;

            _weaponGameObjectSpawner = weaponGameObjectSpawner;
        }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animation = GetComponent<Animation>();
        }

        public void StartThrowing(WeaponFacade weaponFacade)
        {
            gameObject.SetActive(true);

            _spriteRenderer.sprite = weaponFacade.Weapon.PickUp;
            _spriteRenderer.enabled = true;
            _animation.Play();
        }

        public void StopThrowing() => gameObject.SetActive(false);

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent(typeof(IDamageable)) is IDamageable damageable)
            {
                damageable.TakeDamage(_thrownWeapon.Weapon.ThrowDamage);

                if (_fly != null)
                {
                    StopCoroutine(_fly);
                    StartCoroutine(Bounce(-_rigidBody.velocity.x));
                }
            }
        }

        public void Throw(WeaponFacade weaponFacade, Vector2 startPosition, Vector2 endPosition)
        {
            if (weaponFacade == null)
                throw new ArgumentNullException();
            
            _thrownWeapon = weaponFacade;
            
            var polygonBounds = _roomConfigurator.PolygonWallBounds;

            if (!polygonBounds.OverlapPoint(startPosition))
                startPosition = _roomConfigurator.PolygonFloorBorder.ClosestPoint(startPosition);

            transform.position = startPosition;
            var direction = (endPosition - startPosition).normalized;
            _fly = StartCoroutine(Fly(direction));
        }

        private IEnumerator Fly(Vector2 direction)
        {
            _collider.enabled = true;
            
            var timeFlying = 0f;
            
            while (timeFlying < _settings.TimeToFly)
            {
                _rigidBody.velocity += direction * _settings.Speed;
                
                timeFlying += Time.deltaTime;
                yield return null;
            }

            _collider.enabled = false;
            StartCoroutine(Drop());
        }

        private IEnumerator Drop()
        {
            var timeDropping = 0f;
            
            while (timeDropping < _settings.TimeToDrop)
            {
                _rigidBody.velocity += Vector2.down * _settings.DropSpeed;
                
                timeDropping += Time.deltaTime;
                yield return null;
            }

            _rigidBody.velocity = Vector2.zero;
            
            gameObject.SetActive(false);

            _thrownWeapon.Durability -= 0.5f;
            TryCreateWeapon();
        }
        
        private IEnumerator Bounce(float velocityX)
        {
            var velocity = _rigidBody.velocity;
            velocity.x = velocityX;
            _rigidBody.velocity = velocity;
            
            var timeDropping = 0f;
            
            while (timeDropping < _settings.TimeToDrop)
            {
                _rigidBody.velocity += Vector2.down * _settings.DropSpeed;
                
                timeDropping += Time.deltaTime;
                yield return null;
            }

            _rigidBody.velocity = Vector2.zero;
            
            gameObject.SetActive(false);

            _thrownWeapon.Durability -= 0.34f;
            TryCreateWeapon();
        }

        private void TryCreateWeapon()
        {
            if (_thrownWeapon.Durability == 0)
                return;
                        
            _weaponGameObjectSpawner.Spawn(transform.position, _thrownWeapon);
        }
    }
}