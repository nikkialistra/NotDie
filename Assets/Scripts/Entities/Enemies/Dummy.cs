using System;
using Core.StateSystem;
using Entities.Enemies.States;
using UnityEngine;

namespace Entities.Enemies
{
    public class Dummy : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public float Speed;
            public float TimeToStay;
        }

        [SerializeField] private Settings _settings;

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
            var moveToPlayer = new MoveToPlayer(this, _settings.Speed);

            At(moveToPlayer, stay, StayedEnoughTime());

            _stateMachine.SetState(stay);

            void At(State to, State from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);

            Func<bool> StayedEnoughTime() => () => stay.TimeStayed > _settings.TimeToStay;
        }
    }
}