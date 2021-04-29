using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views
{
    [RequireComponent(typeof(DigitManager))]
    public class HpView : MonoBehaviour
    {
        [SerializeField] private Sprite _border;
        [SerializeField] private Sprite _activatedBorderBig;
        [SerializeField] private Sprite _activatedBorderSmall;

        [Space] [SerializeField] private Sprite _fill;
        [SerializeField] private Sprite _whiteFill;
        [SerializeField] private Sprite _activatedFillBig;
        [SerializeField] private Sprite _activatedFillSmall;

        [Space]
        [SerializeField] private Sprite _stitch;
        

        private VisualElement _rootVisualElement;

        private Image _fillIndicator;
        private Image _whiteFillIndicator;
        private Image _activatedFillIndicator;
        private Image _borders;

        private Image _stitchIndicator;

        private Label _lives;

        private DigitManager _digitManager;

        private Image _healthFirst;
        private Image _healthSecond;
        private Image _healthThird;

        private float _fullHealthValue;

        private Coroutine _healthBarCoroutine;

        private void Awake()
        {
            _digitManager = GetComponent<DigitManager>();

            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            _fillIndicator = _rootVisualElement.Q<Image>("fill_indicator");
            _whiteFillIndicator = _rootVisualElement.Q<Image>("white_fill_indicator");
            _activatedFillIndicator = _rootVisualElement.Q<Image>("activated_fill_indicator");
            _borders = _rootVisualElement.Q<Image>("borders");

            _stitchIndicator = _rootVisualElement.Q<Image>("stitch");

            _lives = _rootVisualElement.Q<Label>("lives");

            _healthFirst = _rootVisualElement.Q<Image>("health__first");
            _healthSecond = _rootVisualElement.Q<Image>("health__second");
            _healthThird = _rootVisualElement.Q<Image>("health__third");
        }

        public void SetFullHealthValue(float fullValue)
        {
            _fullHealthValue = fullValue;
        }

        public void SetInitialHealth(float value)
        {
            _borders.sprite = _border;
            _fillIndicator.sprite = _fill;
            _stitchIndicator.sprite = _stitch;

            var fillAmount = value / _fullHealthValue;
            var targetWidth = fillAmount * 100;
            
            _fillIndicator.style.width = targetWidth;
            _activatedFillIndicator.style.width = targetWidth;
            _stitchIndicator.style.left = 100;

            UpdateDigits(value);
        }

        public void SetHealth(float value)
        {
            UpdateDigits(value);

            if (value != _fullHealthValue)
            {
                PlayHealthBarSpritesAnimation(value);
            }
            else
            {
                SetInitialHealth(value);
            }
        }

        private void PlayHealthBarSpritesAnimation(float value)
        {
            if (_healthBarCoroutine != null)
            {
                StopCoroutine(_healthBarCoroutine);
            }
            
            var fillAmount = value / _fullHealthValue;
            var targetWidth = fillAmount * 100;
            
            _healthBarCoroutine = StartCoroutine(PlayHealthBarCoroutine(targetWidth));
        }

        private IEnumerator PlayHealthBarCoroutine(float targetWidth)
        {
            ShowBigWhiteFillIndicator();

            yield return new WaitForSeconds(0.1f);

            ShowMediumWhiteFillIndicator();

            yield return new WaitForSeconds(0.1f);
            
            ShowSmallWhiteIndicatorWithStitch(targetWidth);

            yield return new WaitForSeconds(0.1f);

            ShowRedFillIndicatorWithStitch(targetWidth);
        }

        private void ShowBigWhiteFillIndicator()
        {
            _fillIndicator.style.display = DisplayStyle.None;
            _activatedFillIndicator.style.display = DisplayStyle.Flex;

            _borders.sprite = _activatedBorderBig;
            _activatedFillIndicator.sprite = _activatedFillBig;
        }

        private void ShowMediumWhiteFillIndicator()
        {
            _borders.sprite = _activatedBorderSmall;
            _activatedFillIndicator.sprite = _activatedFillSmall;
        }

        private void ShowSmallWhiteIndicatorWithStitch(float targetWidth)
        {
            _activatedFillIndicator.style.display = DisplayStyle.None;
            _whiteFillIndicator.style.display = DisplayStyle.Flex;

            _borders.sprite = _border;
            _whiteFillIndicator.sprite = _whiteFill;

            _whiteFillIndicator.style.width = targetWidth + 1;
            _stitchIndicator.style.left = targetWidth - 2;
        }

        private void ShowRedFillIndicatorWithStitch(float targetWidth)
        {
            _whiteFillIndicator.style.display = DisplayStyle.None;
            _fillIndicator.style.display = DisplayStyle.Flex;

            _fillIndicator.style.width = targetWidth;
            _stitchIndicator.style.left = targetWidth - 3;

            _activatedFillIndicator.style.width = targetWidth;

            _fillIndicator.sprite = _fill;
        }

        private void UpdateDigits(float value)
        {
            if (value >= 100)
            {
                SetThreeDigits(value);
            }
            else if (value >= 10)
            {
                SetTwoDigits(value);
            }
            else
            {
                SetOneDigit(value);
            }
        }

        private void SetThreeDigits(float value)
        {
            _healthFirst.sprite = _digitManager.GetDigitSprite(value / 100);
            _healthSecond.sprite = _digitManager.GetDigitSprite(value / 10 % 10);
            _healthThird.sprite = _digitManager.GetDigitSprite(value % 10);

            _healthFirst.style.display = DisplayStyle.Flex;
            _healthSecond.style.display = DisplayStyle.Flex;
            _healthThird.style.display = DisplayStyle.Flex;
        }

        private void SetTwoDigits(float value)
        {
            _healthSecond.sprite = _digitManager.GetDigitSprite(value / 10);
            _healthThird.sprite = _digitManager.GetDigitSprite(value % 10);

            _healthFirst.style.display = DisplayStyle.None;
            _healthSecond.style.display = DisplayStyle.Flex;
            _healthThird.style.display = DisplayStyle.Flex;
        }

        private void SetOneDigit(float value)
        {
            _healthThird.sprite = _digitManager.GetDigitSprite(value % 10);

            _healthFirst.style.display = DisplayStyle.None;
            _healthSecond.style.display = DisplayStyle.None;
            _healthThird.style.display = DisplayStyle.Flex;
        }

        public void SetLives(int value)
        {
            _lives.text = value.ToString();
        }
    }
}