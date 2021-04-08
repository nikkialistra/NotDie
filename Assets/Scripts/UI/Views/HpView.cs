using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views
{
    [RequireComponent(typeof(DigitManager))]
    public class HpView : MonoBehaviour
    {
        private VisualElement _rootVisualElement;

        private Image _fillIndicator;

        private Label _lives;

        private DigitManager _digitManager;
        private Image _healthFirst;
        private Image _healthSecond;
        private Image _healthThird;

        private float _fullHealthValue;

        private void Awake()
        {
            _digitManager = GetComponent<DigitManager>();
            
            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            _fillIndicator = _rootVisualElement.Q<Image>("fill_indicator");

            _lives = _rootVisualElement.Q<Label>("lives");

            _healthFirst = _rootVisualElement.Q<Image>("health__first");
            _healthSecond = _rootVisualElement.Q<Image>("health__second");
            _healthThird = _rootVisualElement.Q<Image>("health__third");
        }

        public void SetFullHealthValue(int fullValue) => _fullHealthValue = fullValue;

        public void SetHealth(int value)
        {
            //_fillIndicator.fillAmount = value / _fullHealthValue;

            if (value >= 100)
                SetThreeDigits(value);
            else if (value >= 10)
                SetTwoDigits(value);
            else
                SetOneDigit(value);
        }

        private void SetThreeDigits(int value)
        {
            _healthFirst.sprite = _digitManager.GetDigit(value / 100);
            _healthSecond.sprite = _digitManager.GetDigit(value / 10 % 10);
            _healthThird.sprite = _digitManager.GetDigit(value % 10);

            _healthFirst.EnableInClassList("not_displayed", false);
            _healthSecond.EnableInClassList("not_displayed", false);
            _healthThird.EnableInClassList("not_displayed", false);
        }

        private void SetTwoDigits(int value)
        {
            _healthSecond.sprite = _digitManager.GetDigit(value / 10);
            _healthThird.sprite = _digitManager.GetDigit(value % 10);
            
            _healthFirst.EnableInClassList("not_displayed", true);
            _healthSecond.EnableInClassList("not_displayed", false);
            _healthThird.EnableInClassList("not_displayed", false);
        }

        private void SetOneDigit(int value)
        {
            _healthThird.sprite = _digitManager.GetDigit(value % 10);
            
            _healthFirst.EnableInClassList("not_displayed", true);
            _healthSecond.EnableInClassList("not_displayed", true);
            _healthThird.EnableInClassList("not_displayed", false);
        }

        public void SetLives(int value) => _lives.text = value.ToString();
    }
}