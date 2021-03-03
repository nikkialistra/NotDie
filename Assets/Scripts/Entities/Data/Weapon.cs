using UnityEngine;

namespace Entities.Data
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
    public class Weapon : ScriptableObject
    {
        [Header("Icons")] 
        public Sprite PickUp;
        
        public Sprite Active;
        public Sprite NotActive;
        
        [Header("Specs")]
        public int Damage;

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (Damage < 0)
                Damage = 0;
        }
        #endif
    }
}