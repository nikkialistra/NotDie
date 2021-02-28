using Pools;
using UnityEngine;

namespace Wave
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _placePoint;

        [SerializeField] private WavePool _wavePool;

        [SerializeField] private Wave _wave;

        private bool _placerIsVisible;
        private Renderer _placerRenderer;

        private void Awake()
        {
            _placerRenderer = _placePoint.GetComponent<Renderer>();
        }

        private void Update()
        {
            _placerRenderer.enabled = _placerIsVisible;
        
            if (Input.GetKeyDown(KeyCode.Space))
                SpawnWave();

            if (Input.GetKeyDown(KeyCode.R))
                _placerIsVisible = !_placerIsVisible;
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