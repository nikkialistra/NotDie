using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Live : MonoBehaviour
    {
        [SerializeField] private Image _lives;

        [SerializeField] private int _numberOfLives;

        [SerializeField] private DigitManager _digitManager;

        private void Start() => SetValue(_numberOfLives);

        public int GetValue() => _numberOfLives;

        public void SetValue(int value)
        {
            if (value < 0 || value > 9)
                throw new ArgumentException("Lives should be between 0 and 9");

            _numberOfLives = value;

            var sprite = _digitManager.GetDigit(value);

            _lives.sprite = sprite;
        }
    }
}