using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    [RequireComponent(typeof(Text))]
    public class TimerView : MonoBehaviour
    {
        private Text _timer;

        private float _time;
        private bool _isWorking;

        private void Awake()
        {
            _timer = GetComponent<Text>();
        }

        private void Start()
        {
            _isWorking = true;
        }

        public void Update()
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

            _timer.text = minutes.ToString("00") + "." + seconds.ToString("00") + "." + milliseconds.ToString("00");
        }
    }
}