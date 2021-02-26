﻿using System;
using Pools.Contracts;
using UnityEngine;

namespace Wave
{
    
    [RequireComponent(typeof(WaveMover))]
    public class WaveFacade : MonoBehaviour, IGameObjectPooled<WaveFacade>
    {
        private WaveMover _waveMover;

        private float _timeToDestroy;
        private int _damageValue;

        public int DamageValue => _damageValue;

        private void Awake()
        {
            _waveMover = GetComponent<WaveMover>();
        }

        private void Update()
        {
            if (_timeToDestroy <= 0)
                return;
            
            _timeToDestroy -= Time.deltaTime;
            if (_timeToDestroy <= 0)
                Disable();
        }
        
        public IPool<WaveFacade> Pool { get; set; }

        public void Initialize(Vector3 position, Quaternion rotation, Vector2 direction, WaveStats waveStats)
        {
            transform.position = position;
            transform.rotation = rotation;

            _waveMover.SetVelocity(waveStats.Velocity);
            _waveMover.SetDirection(direction);
            
            _timeToDestroy = waveStats.TimeToDestroy;

            _damageValue = waveStats.DamageValue;
            
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            Pool.ReturnToPool(this);
        }
    }
}