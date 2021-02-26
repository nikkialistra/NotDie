using UnityEngine;
using Wave;

namespace Room
{
    public class WaveAbsorber : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            var wave = other.gameObject.GetComponent<WaveFacade>();
            if (wave == null) return;
            
            wave.Disable();
        }
    }
}