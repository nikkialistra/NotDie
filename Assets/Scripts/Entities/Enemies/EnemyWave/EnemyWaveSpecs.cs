using Entities.Enemies.Species;
using UnityEngine;

namespace Entities.Enemies.EnemyWave
{
    public class EnemyWaveSpecs
    {
        public Enemy Enemy;
        public int Id;
        public Vector3 Position;
        public Vector3 DirectionDistance;
        public int Damage;
        public bool isPenetrable;
        public GameObject Prefab;
    }
}