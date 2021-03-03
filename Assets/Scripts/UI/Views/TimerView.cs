using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class TimerView : IInitializable, ITickable
    {
        private Text _timer;

        private float _time;
        private bool _isWorking;
        
        public TimerView([Inject(Id = "timer")] Text timer) => _timer = timer;

        public void Initialize() => _isWorking = true;

        public void Tick()
        {
            if (!_isWorking) 
                return;
            
            _time += Time.deltaTime;
            SetTime();
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