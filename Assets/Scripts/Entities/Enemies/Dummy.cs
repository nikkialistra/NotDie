using System;
using Core.StateSystem;
using Entities.Enemies.States;
using Entities.Player;
using UnityEngine;
using Zenject;

namespace Entities.Enemies
{
    [RequireComponent(typeof(DummyAnimator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyHealthHandler))]
    public class Dummy : MonoBehaviour
    {
        [Serializable]
        public class Settings
        {
            public float Speed;
            public float TimeInStay;
            public float TimeInStun;
        }

        private DummyAnimator _dummyAnimator;
        private Rigidbody2D _rigidBody;
        private EnemyHealthHandler _enemyHealthHandler;

        private PlayerMover _player;

        private Settings _settings;

        private StateMachine _stateMachine;

        [Inject]
        public void Construct(Settings settings) => _settings = settings;

        private void Awake()
        {
            _dummyAnimator = GetComponent<DummyAnimator>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _enemyHealthHandler = GetComponent<EnemyHealthHandler>();

            _player = FindObjectOfType<PlayerMover>();

            _stateMachine = new StateMachine();

            SetupStateMachine();
        }

        private void Update() => _stateMachine.Tick();

        private void SetupStateMachine()
        {
            var stay = new Stay();
            var moveToPlayer = new MoveToPlayer(_settings.Speed, _rigidBody, _dummyAnimator, _player);
            var recline = new Recline(_rigidBody, _enemyHealthHandler, _player);
            var stun = new Stun(_dummyAnimator);

            At(moveToPlayer, stay, EnoughTimeInStay());
            At(stun, recline, IsReclined());
            At(moveToPlayer, stun, EnoughTimeInStun());
            
            AtAny(recline, ShouldRecline());

            _stateMachine.SetState(stay);

            void At(State to, State from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            void AtAny(State to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            Func<bool> EnoughTimeInStay() => () => stay.TimeInStay > _settings.TimeInStay;
            Func<bool> IsReclined() => () => recline.IsReclined;
            Func<bool> EnoughTimeInStun() => () => stun.TimeInStun > _settings.TimeInStun;
            
            Func<bool> ShouldRecline() => () => _enemyHealthHandler.ShouldRecline;
        }
    }
}