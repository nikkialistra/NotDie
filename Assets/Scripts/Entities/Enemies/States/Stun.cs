using Core.StateSystem;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class Stun : State
    {
        public float TimeInStun { get; private set; }
        
        private readonly EnemyAnimator _enemyAnimator;

        public Stun(EnemyAnimator enemyAnimator)
        {
            _enemyAnimator = enemyAnimator;
        }

        public override void Tick()
        {
            TimeInStun += Time.deltaTime;
        }

        public override void OnEnter()
        {
            TimeInStun = 0;
            _enemyAnimator.Stun();
        }
    }
}