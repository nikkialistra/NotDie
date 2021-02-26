using UnityEngine;

namespace Wave
{
    public class WaveStats : MonoBehaviour
    {
        [SerializeField] private int _damageValue;

        public int DamageValue => _damageValue;
    }
}