using UnityEngine;
using UnityEngine.UIElements;

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

        private void Awake()
        {
            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            
            _timer = _rootVisualElement.Q<Label>("timer");
        }

        private void Update()
        {
            if (!_isWorking) 
                return;

            _time += Time.deltaTime;

            if (_timeForNextUpdate > Time.time) 
                return;
            
            SetTime();
            
            _timeForNextUpdate = Time.time + _updateTime;
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