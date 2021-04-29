using System;
using Core.StateSystem;
using Entities.Enemies.States;
using Entities.Player;
using UnityEngine;
using Zenject;

namespace Entities.Enemies.Species
{
    [RequireComponent(typeof(EnemyAnimator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EnemyHealthHandler))]
    [RequireComponent(typeof(EnemyAttack))]
    public class Poor : Enemy
    {
        [Serializable]
        public class Settings
        {
            public int Health;
            public float Speed;
            [Space] 
            public Things.Data.EnemyWave EnemyWave;
            [Space]
            public float AttackDistance;
            public float WaveDistance;
            public float AttackCooldown;
            [Space]
            public float TimeInStay;
            public float TimeInStun;
            [Space] 
            public float YPosition;
        }

        public override Vector3 PositionCenter => transform.position + new Vector3(0, _settings.YPosition);

        private EnemyAnimator _enemyAnimator;
        private Rigidbody2D _rigidBody;
        private EnemyHealthHandler _enemyHealthHandler;
        private EnemyAttack _enemyAttack;

        private PlayerMover _player;

        private Settings _settings;

        private StateMachine _stateMachine;

        [Inject]
        public void Construct(Settings settings)
        {
            _settings = settings;
        }

        private void Awake()
        {
            _enemyAnimator = GetComponent<EnemyAnimator>();
            _rigidBody = GetComponent<Rigidbody2D>();
            _enemyHealthHandler = GetComponent<EnemyHealthHandler>();
            _enemyAttack = GetComponent<EnemyAttack>();

            _player = FindObjectOfType<PlayerMover>();

            _stateMachine = new StateMachine();

            SetupStateMachine();
        }

        private void Start()
        {
            _enemyHealthHandler.Health = _settings.Health;
            _enemyAttack.EnemyWave = _settings.EnemyWave;
            _enemyAttack.Cooldown = _settings.AttackCooldown;
            _enemyAttack.WaveDistance = _settings.WaveDistance;
        }

        private void FixedUpdate()
        {
            _stateMachine.Tick();
        }

        private void SetupStateMachine()
        {
            var stay = new Stay();
            var moveToPlayer = new MoveToPlayer(_settings.Speed, _rigidBody, _enemyAnimator, _player);
            var moveAndAttack = new MoveAndAttack(_settings.Speed, _rigidBody, _enemyAnimator, _player, _enemyAttack);
            var recline = new Recline(_rigidBody, _enemyHealthHandler, _player);
            var stun = new Stun(_enemyAnimator);

            At(moveToPlayer, stay, EnoughTimeInStay());
            
            At(moveAndAttack, moveToPlayer, PlayerIsClose());
            At(moveToPlayer, moveAndAttack, PlayerIsNotClose());
            
            At(stun, recline, IsNotReclined());
            At(moveToPlayer, stun, EnoughTimeInStun());
            
            AtAny(recline, ShouldRecline());

            _stateMachine.SetState(stay);

            void At(State to, State from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            void AtAny(State to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            Func<bool> EnoughTimeInStay() => () => stay.TimeInStay > _settings.TimeInStay;
            
            Func<bool> PlayerIsClose() => () => (_player.transform.position - _rigidBody.transform.position).magnitude < _settings.AttackDistance;
            Func<bool> PlayerIsNotClose() => () => (_player.transform.position - _rigidBody.transform.position).magnitude >= _settings.AttackDistance;
            
            Func<bool> IsNotReclined() => () => !recline.IsReclined;
            Func<bool> EnoughTimeInStun() => () => stun.TimeInStun > _settings.TimeInStun;
            
            Func<bool> ShouldRecline() => () => _enemyHealthHandler.ShouldRecline;
        }
    }
}