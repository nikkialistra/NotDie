using Core.StateSystem;
using Entities.Player;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class MoveToPlayer : State
    {
        private Dummy _dummy;
        private Rigidbody2D _rigidBody;
        private PlayerMover _player;
        private float _speed;

        public MoveToPlayer(Dummy dummy, float speed)
        {
            _dummy = dummy;
            _rigidBody = _dummy.GetComponent<Rigidbody2D>();
            _speed = speed;
        }
        
        public override void Tick()
        {
            var direction = (_player.transform.position - _dummy.transform.position).normalized;
            _dummy.MoveDirection = direction;
            
            _rigidBody.velocity += (Vector2) direction * (_speed * Time.fixedDeltaTime);
        }

        public override void OnEnter()
        {
            _player = Object.FindObjectOfType<PlayerMover>();
        }
    }
}