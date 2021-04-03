using UnityEngine;

namespace Core.Room
{
    public class Damager : MonoBehaviour
    {
        [SerializeField] private int _value;

        public int Value => _value;
    }
}