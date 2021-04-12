using Core.StateSystem;
using DG.Tweening;
using Entities.Player;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class Recline : State
    {
        public bool IsReclined { get; private set; }
        
        private readonly Rigidbody2D _rigidBody;
        private readonly EnemyHealthHandler _enemyHealthHandler;
        private readonly PlayerMover _player;

        public Recline(Rigidbody2D rigidBody, EnemyHealthHandler enemyHealthHandler, PlayerMover player)
        {
            _rigidBody = rigidBody;
            _enemyHealthHandler = enemyHealthHandler;
            _player = player;
        }

        public override void OnEnter()
        {
            IsReclined = true;
            
            var direction = (_rigidBody.position - (Vector2) _player.transform.position).normalized;
            var reclineValue = _enemyHealthHandler.HandleRecline();
            
            var velocity = direction * reclineValue;
            
            DOTween.To(() => _rigidBody.velocity, x => _rigidBody.velocity = x, velocity, 0.25f).SetEase(Ease.OutQuint)
                .OnComplete(() => IsReclined = false);
        }
    }
}