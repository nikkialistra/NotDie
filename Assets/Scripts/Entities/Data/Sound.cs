using UnityEngine;

namespace Entities.Data
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Data/Sound", order = 2)]
    public class Sound : ScriptableObject
    {
        public AudioClip Clip;
        
        [Range(0, 1)] 
        public float Volume = 1;

        [Range(.1f, 3)] 
        public float Pitch = 1;
        
        public bool Loop;
        
        private AudioSource _source;

        public void CreateAudioSource(GameObject gameObject)
        {
            _source = gameObject.AddComponent<AudioSource>();
            _source.clip = Clip;
            _source.volume = Volume;
            _source.pitch = Pitch;
            _source.loop = Loop;
        }

        public void Play()
        {
            if (!_source.isPlaying)
                _source.Play();
        }

        public void MultiplePlay() => _source.PlayOneShot(_source.clip);

        public void Stop() => _source.Stop();
    }
}