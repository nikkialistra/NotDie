using Core.StateSystem;
using Entities.Player;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class Recline : State
    {
        public bool IsReclined => _rigidBody.velocity.magnitude < 0.1f;
        
        private Rigidbody2D _rigidBody;
        private EnemyHealthHandler _enemyHealthHandler;
        private PlayerMover _player;

        public Recline(Rigidbody2D rigidBody, EnemyHealthHandler enemyHealthHandler, PlayerMover player)
        {
            _rigidBody = rigidBody;
            _enemyHealthHandler = enemyHealthHandler;
            _player = player;
        }

        public override void OnEnter()
        {
            var direction = (_rigidBody.position - (Vector2) _player.transform.position).normalized;

            var reclineValue = _enemyHealthHandler.HandleRecline();

            _rigidBody.velocity = direction * reclineValue;
        }
    }
}