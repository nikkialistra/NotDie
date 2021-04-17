using Core.StateSystem;
using Entities.Player;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class MoveToPlayer : State
    {
        private readonly Rigidbody2D _rigidBody;
        private readonly EnemyAnimator _enemyAnimator;
        private readonly PlayerMover _player;
        
        private readonly float _speed;

        public MoveToPlayer(float speed, Rigidbody2D rigidBody, EnemyAnimator enemyAnimator, PlayerMover player)
        {
            _rigidBody = rigidBody;
            _enemyAnimator = enemyAnimator;
            _speed = speed;
            _player = player;
        }
        
        public override void Tick()
        {
            var direction = (_player.transform.position - _rigidBody.transform.position).normalized;
            _enemyAnimator.UpdateDirection(direction);

            _rigidBody.velocity += (Vector2) direction * (_speed * Time.fixedDeltaTime);
        }
    }
}