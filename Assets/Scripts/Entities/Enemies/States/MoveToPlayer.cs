using Core.StateSystem;
using Entities.Player;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class MoveToPlayer : State
    {
        private Rigidbody2D _rigidBody;
        private DummyAnimator _dummyAnimator;
        private PlayerMover _player;
        private float _speed;

        public MoveToPlayer(float speed, Rigidbody2D rigidBody, DummyAnimator dummyAnimator, PlayerMover player)
        {
            _rigidBody = rigidBody;
            _dummyAnimator = dummyAnimator;
            _speed = speed;
            _player = player;
        }
        
        public override void Tick()
        {
            var direction = (_player.transform.position - _rigidBody.transform.position).normalized;
            _dummyAnimator.UpdateDirection(direction);

            _rigidBody.velocity += (Vector2) direction * (_speed * Time.fixedDeltaTime);
        }
    }
}