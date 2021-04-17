using UnityEngine;

namespace Things.Data
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "Data/EnemyWave")]
    public class EnemyWave : ScriptableObject
    {
        public GameObject WavePrefab;
        [Space]
        public int Damage;
        public float WaveDelay;
        public bool isPenetrable;
    }
}