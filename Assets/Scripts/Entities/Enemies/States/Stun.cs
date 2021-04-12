using Core.StateSystem;
using UnityEngine;

namespace Entities.Enemies.States
{
    public class Stun : State
    {
        public float TimeInStun { get; private set; }
        
        private readonly DummyAnimator _dummyAnimator;

        public Stun(DummyAnimator dummyAnimator) => _dummyAnimator = dummyAnimator;

        public override void Tick() => TimeInStun += Time.deltaTime;

        public override void OnEnter()
        {
            TimeInStun = 0;
            _dummyAnimator.Stun();
        }
    }
}