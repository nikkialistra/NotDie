using System;
using Core.Saving;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace UI.Views
{
    [RequireComponent(typeof(UIDocument))]
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private float _updateTime;
        
        private VisualElement _rootVisualElement;

        private Label _timer;

        private float _time;
        private bool _isWorking = true;

        private float _timeForNextUpdate;
        
        private GameSettings _gameSettings;

        [Inject]
        public void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        private void Awake()
        {
            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            
            _timer = _rootVisualElement.Q<Label>("timer");

            _gameSettings.Change += OnSettingsChange;
        }

        private void Start()
        {
            if (_gameSettings.ShowTimer == ShowTimer.False)
            {
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (!_isWorking)
            {
                return;
            }

            _time += Time.deltaTime;

            if (_timeForNextUpdate > Time.time)
            {
                return;
            }
            
            SetTime();
            
            _timeForNextUpdate = Time.time + _updateTime;
        }

        private void OnSettingsChange()
        {
            if (_gameSettings.ShowTimer == ShowTimer.False)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

        public void StartTimer()
        {
            _isWorking = true;
        }

        public void StopTimer()
        {
            _isWorking = false;
        }

        public void ResetTimer()
        {
            _time = 0;
        }

        private void SetTime()
        {
            var minutes = (int) (_time / 60f) % 60;
            var seconds = (int) (_time % 60f);
            var milliseconds = (int) (_time * 100f) % 100;

            _timer.text = $"{minutes:00}.{seconds:00}.{milliseconds:00}";
        }
    }
}