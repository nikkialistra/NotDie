using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class HpView
    {
        private Image _fillIndicator;
        
        private Text _lives;
        
        private DigitManager _digitManager;
        private Image _healthFirst;
        private Image _healthSecond;
        private Image _healthThird;

        private float _fullHealthValue;

        public HpView(Image fillIndicator, [Inject(Id = "lives")] Text lives, DigitManager digitManager,
            [Inject(Id = "healthFirst")] Image healthFirst, 
            [Inject(Id = "healthSecond")] Image healthSecond,
            [Inject(Id = "healthThird")] Image healthThird)
        {
            _fillIndicator = fillIndicator;
            _lives = lives;

            _digitManager = digitManager;

            _healthFirst = healthFirst;
            _healthSecond = healthSecond;
            _healthThird = healthThird;
        }

        public void SetFullHealthValue(int fullValue) => _fullHealthValue = fullValue;

        public void SetHealth(int value)
        {
            _fillIndicator.fillAmount = value / _fullHealthValue;

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
        }

        private void SetTwoDigits(int value)
        {
            _healthFirst.gameObject.SetActive(false);
            _healthSecond.sprite = _digitManager.GetDigit(value / 10);
            _healthThird.sprite = _digitManager.GetDigit(value % 10);
        }

        private void SetOneDigit(int value)
        {
            _healthFirst.gameObject.SetActive(false);
            _healthSecond.gameObject.SetActive(false);
            _healthThird.sprite = _digitManager.GetDigit(value % 10);
        }

        public void SetLives(int value) => _lives.text = value.ToString();
    }
}