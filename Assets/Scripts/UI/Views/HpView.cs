using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

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
        private Image _health;

        private Image _stitchIndicator;
        
        private Label _lives;

        private DigitManager _digitManager;

        private Image _healthFirst;
        private Image _healthSecond;
        private Image _healthThird;

        private int _fullHealthValue;

        private Coroutine _healthBarCoroutine;

        private void Awake()
        {
            _digitManager = GetComponent<DigitManager>();

            _rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            _fillIndicator = _rootVisualElement.Q<Image>("fill_indicator");
            _whiteFillIndicator = _rootVisualElement.Q<Image>("white_fill_indicator");
            _health = _rootVisualElement.Q<Image>("health");

            _stitchIndicator = _rootVisualElement.Q<Image>("stitch");

            _lives = _rootVisualElement.Q<Label>("lives");

            _healthFirst = _rootVisualElement.Q<Image>("health__first");
            _healthSecond = _rootVisualElement.Q<Image>("health__second");
            _healthThird = _rootVisualElement.Q<Image>("health__third");
        }

        public void SetFullHealthValue(int fullValue) => _fullHealthValue = fullValue;

        public void SetInitialHealth(int value)
        {
            _health.sprite = _border;
            _fillIndicator.sprite = _fill;
            _stitchIndicator.sprite = _stitch;

            var fillAmount = value / _fullHealthValue;
            _fillIndicator.style.width = fillAmount * 100;
            _stitchIndicator.style.left = 100;

            UpdateDigits(value);
        }

        public void SetHealth(int value)
        {
            PlayFillAnimation(value);

            UpdateDigits(value);

            if (value != _fullHealthValue)
                PlayHealthBarSpritesAnimation();
            else
                _stitchIndicator.style.left = 100;
        }

        private void PlayFillAnimation(int value)
        {
            var fillAmount = (float) value / _fullHealthValue;
            _fillIndicator.experimental.animation.Start(
                new StyleValues {width = fillAmount * 100}, 300);

            _whiteFillIndicator.experimental.animation.Start(
                new StyleValues {width = fillAmount * 100}, 300);
            
            _stitchIndicator.style.left = fillAmount * 100 - 3;
        }

        private void PlayHealthBarSpritesAnimation()
        {
            if (_healthBarCoroutine != null)
                StopCoroutine(_healthBarCoroutine);
            
            _healthBarCoroutine = StartCoroutine(PlayHealthBarCoroutine());
        }

        private IEnumerator PlayHealthBarCoroutine()
        {
            _fillIndicator.EnableInClassList("not_displayed", true);
            _whiteFillIndicator.EnableInClassList("not_displayed", false);

            _health.sprite = _activatedBorderBig;
            _whiteFillIndicator.sprite = _activatedFillBig;

            yield return new WaitForSeconds(0.12f);

            _health.sprite = _activatedBorderSmall;
            _whiteFillIndicator.sprite = _activatedFillSmall;

            yield return new WaitForSeconds(0.08f);

            _health.sprite = _border;
            _whiteFillIndicator.sprite = _whiteFill;

            yield return new WaitForSeconds(0.06f);

            _fillIndicator.EnableInClassList("not_displayed", false);
            _whiteFillIndicator.EnableInClassList("not_displayed", true);

            _fillIndicator.sprite = _fill;
        }

        private void UpdateDigits(int value)
        {
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

        private static Texture2D TextureFromSprite(Sprite sprite)
        {
            var width = (int) sprite.rect.width;
            var height = (int) sprite.rect.height;

            var widthOffset = (int) sprite.textureRect.x;
            var heightOffset = (int) sprite.textureRect.y;

            var texture = new Texture2D(width, height);

            for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
                texture.SetPixel(i, j, sprite.texture.GetPixel(widthOffset + i, heightOffset + j));

            texture.filterMode = FilterMode.Point;
            texture.Apply();

            return texture;
        }
    }
}