using Entities.Enemies.Species;
using UnityEngine;

namespace Entities.Enemies.EnemyWave
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyWaveMover : MonoBehaviour
    {
        private Enemy _enemy;
        private EnemyAnimator _enemyAnimator;

        private Rigidbody2D _enemyRigidbody;

        private Rigidbody2D _rigidBody;

        private void Awake() => _rigidBody = GetComponent<Rigidbody2D>();

        private void FixedUpdate()
        {
            if (_enemyRigidbody == null)
                return;
            
            _rigidBody.velocity = _enemyRigidbody.velocity;
        }

        public void SetEnemy(Enemy enemy)
        {
            _enemy = enemy;
            _enemyAnimator = enemy.GetComponent<EnemyAnimator>();
        }

        public void SetDirection(Vector3 position, Vector3 direction)
        {
            _enemyRigidbody= _enemy.GetComponent<Rigidbody2D>();
            
            if (direction == Vector3.zero) 
                return;

            transform.position = position + direction;

            int additionalRotation;

            if (_enemyAnimator.IsFlipped)
            {
                Flip();
                additionalRotation = 180;
            }
            else
            {
                transform.localScale = Vector3.one;
                additionalRotation = 0;
            }

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + additionalRotation, Vector3.forward);
        }
        
        private void Flip()
        {
            var scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
    }
}