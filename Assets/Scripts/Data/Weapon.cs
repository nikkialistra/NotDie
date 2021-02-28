using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon", order = 1)]
    public class Weapon : ScriptableObject
    {
        [Header("Icons")]
        public Sprite Active;
        public Sprite NotActive;
        
        [Header("Specs")]
        public int Damage;

        private void OnValidate()
        {
            if (Damage < 0)
                Damage = 0;
        }
    }
}