using Services.Pools;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wave
{
    [RequireComponent(typeof(PlayerInput))]
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _placePoint;

        [SerializeField] private WavePool _wavePool;

        [SerializeField] private Data.Wave _wave;

        private bool _placerIsVisible;
        private Renderer _placerRenderer;
        
        private PlayerInput _input;
        private InputAction _attackAction;
        private InputAction _showAttackDirectionAction;

        private void Awake()
        {
            _placerRenderer = _placePoint.GetComponent<Renderer>();
            
            _input = GetComponent<PlayerInput>();
            _attackAction = _input.actions.FindAction("Attack");
            _showAttackDirectionAction = _input.actions.FindAction("ShowAttackDirection");
        }

        private void OnEnable()
        {
            _attackAction.started += OnAttack;
            _showAttackDirectionAction.started += OnShowAttackDirection;
        }

        private void OnDisable()
        {
            _attackAction.started -= OnAttack;
            _showAttackDirectionAction.started -= OnShowAttackDirection;
        }

        private void OnAttack(InputAction.CallbackContext context) => SpawnWave();

        private void OnShowAttackDirection(InputAction.CallbackContext context)
        {
            _placerIsVisible = !_placerIsVisible;
            _placerRenderer.enabled = _placerIsVisible;
        }

        private void SpawnWave()
        {
            var transformPosition = transform.position;
        
            var direction = (_placePoint.position - transformPosition).normalized;

            var wave = _wavePool.Get();
            wave.Initialize(_placePoint.transform.position, Quaternion.identity, direction, _wave);
        }
    }
}