﻿using Pools;
using UnityEngine;

namespace Wave
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _placePoint;

        [SerializeField] private WavePool _wavePool;
        [SerializeField] private float _timeToDestroy;

        private bool _placerIsVisible;
        private Renderer _placerRenderer;

        private void Awake()
        {
            _placerRenderer = _placePoint.GetComponent<Renderer>();
        }

        private void Update()
        {
            _placerRenderer.enabled = _placerIsVisible;
        
            if (Input.GetMouseButtonDown(0))
                SpawnWave();

            if (Input.GetKeyDown(KeyCode.R))
                _placerIsVisible = !_placerIsVisible;
        }

        private void SpawnWave()
        {
            var transformPosition = transform.position;
        
            var direction = (_placePoint.position - transformPosition).normalized;

            var wave = _wavePool.Get();
            wave.transform.position = _placePoint.transform.position;
            wave.transform.rotation = Quaternion.identity;
            
            wave.gameObject.SetActive(true);
            
            wave.SetDirection(direction);
            wave.SetTimeToDestroy(_timeToDestroy);
        }
    }
}