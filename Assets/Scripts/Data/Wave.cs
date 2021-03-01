using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Data/Wave", order = 0)]
    public class Wave : ScriptableObject
    {
        public int DamageValue;
        public float Velocity;
        public float TimeToDestroy;

        private void OnValidate()
        {
            if (DamageValue < 0)
                DamageValue = 0;
            
            if (Velocity < 0)
                Velocity = 0;
            
            if (TimeToDestroy < 0)
                TimeToDestroy = 0;
        }
    }
}