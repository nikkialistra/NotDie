using UnityEngine;

namespace Entities.Enemies.Species
{
    public abstract class Enemy : MonoBehaviour
    {
        public abstract Vector3 PositionCenter { get; }
    }
}