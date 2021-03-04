using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class TimerView : IInitializable, ITickable
    {
        [Serializable]
        public class Settings
        {
            public float UpdateTime;
        }

        private Settings _settings;
        
        private Text _timer;

        private float _time;
        private bool _isWorking;

        private float _timeForNextUpdate;
        
        public TimerView(Settings settings, [Inject(Id = "timer")] Text timer)
        {
            _settings = settings;
            _timer = timer;
        }

        public void Initialize() => _isWorking = true;

        public void Tick()
        {
            if (!_isWorking) 
                return;
            
            _time += Time.deltaTime;

            if (_timeForNextUpdate > Time.time) 
                return;
            
            SetTime();
            
            _timeForNextUpdate = Time.time + _settings.UpdateTime;
        }

        public void StartTimer() => _isWorking = true;

        public void StopTimer() => _isWorking = false;

        public void ResetTimer() => _time = 0;

        private void SetTime()
        {
            var minutes = (int) (_time / 60f) % 60;
            var seconds = (int) (_time % 60f);
            var milliseconds = (int) (_time * 100f) % 100;

            _timer.text = $"{minutes:00}.{seconds:00}.{milliseconds:00}";
        }

        
    }
}