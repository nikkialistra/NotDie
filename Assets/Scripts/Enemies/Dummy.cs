using System;
using Enemies.States;
using UnityEngine;
using StateSystem;

namespace Enemies
{
    public class Dummy : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _timeToStay;
        
        
        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new StateMachine();

            SetupStateMachine();
        }

        private void Update() => _stateMachine.Tick();

        private void SetupStateMachine()
        {
            var stay = new Stay();
            var moveToPlayer = new MoveToPlayer(this, _speed);

            At(moveToPlayer, stay, StayedEnoughTime());

            _stateMachine.SetState(stay);

            void At(State to, State from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> StayedEnoughTime() => () => stay.TimeStayed > _timeToStay;
        }
    }
}