using UnityEngine;

namespace Wave
{
    [CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 0)]
    public class WaveStats : ScriptableObject
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