using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private Image _leftWeapon;
        [SerializeField] private Image _rightWeapon;

        [SerializeField] private Sprite _handActive;
        [SerializeField] private Sprite _handNotActive;
        

        private bool _leftIsActive;

        private void Start()
        {
            SwitchWeapon();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _leftIsActive = !_leftIsActive;
                SwitchWeapon();
            }
        }

        private void SwitchWeapon()
        {
            if (_leftIsActive)
            {
                _leftWeapon.sprite = _handActive;
                _rightWeapon.sprite = _handNotActive;
            }
            else
            {
                _leftWeapon.sprite = _handNotActive;
                _rightWeapon.sprite = _handActive;
            }
        }
    }
}