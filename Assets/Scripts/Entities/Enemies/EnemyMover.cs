using DG.Tweening;
using UnityEngine;

namespace Entities.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMover : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody2D>();

        public void AddVelocity(Vector3 velocity, AnimationCurve curve, float time)
        {
            DOTween.To(() => (Vector3) _rigidbody.velocity, Setter, velocity, time).SetEase(curve);

            void Setter(Vector3 x)
            {
                if (_rigidbody == null)
                    return;
                _rigidbody.velocity = x;
            }
        }
    }
}
