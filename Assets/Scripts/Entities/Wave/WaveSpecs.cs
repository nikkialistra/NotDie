using UnityEngine;

namespace Entities.Wave
{
    public class WaveSpecs
    {
        public int Id;
        public Transform Transform;
        public Vector3 Direction;
        public int WaveTriggerName;
        public int Damage;
        public bool isPenetrable;
        public GameObject Prefab;
    }
}