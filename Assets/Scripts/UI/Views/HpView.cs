using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class HpView : MonoBehaviour
    {
        [SerializeField] private Image _fillIndicator;
        
        [SerializeField] private Text _lives;
        
        [SerializeField] private Text _health;
        [SerializeField] private Text _healthShadow;

        private float _fullHealthValue;

        public void SetFullHealthValue(int fullValue) => _fullHealthValue = fullValue;

        public void SetHealth(int value)
        {
            _fillIndicator.fillAmount = value / _fullHealthValue;
            
            _health.text = value.ToString();
        }
        
        public void SetLives(int value)
        {
            _lives.text = value.ToString();
        }

    }
}