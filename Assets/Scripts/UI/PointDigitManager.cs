using UnityEngine;

namespace UI
{
    public class PointDigitManager : DigitManager
    {
        [SerializeField] private Sprite _point;

        public Sprite GetPoint()
        {
            return _point;
        }
    }
}