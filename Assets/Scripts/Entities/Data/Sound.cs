using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Data
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Data/Sound", order = 2)]
    public class Sound : ScriptableObject
    {
        public AudioClip Clip;
        
        [Range(0, 1)] 
        public float Volume = 1;

        [Range(.1f, 3)] 
        public float MinPitch = 1;
        [Range(.1f, 3)] 
        public float MaxPitch = 1;
        
        public bool Loop;
        
        private AudioSource _source;

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (MaxPitch < MinPitch)
                MaxPitch = MinPitch;
        }
        #endif

        public void CreateAudioSource(GameObject gameObject)
        {
            _source = gameObject.AddComponent<AudioSource>();
            _source.clip = Clip;
            _source.volume = Volume;
            _source.loop = Loop;
        }

        public void Play()
        {
            if (_source.isPlaying)
                return;

            if (Math.Abs(MinPitch - MaxPitch) > 0.01) 
                _source.pitch = Random.Range(MinPitch, MaxPitch);

            _source.Play();
        }

        public void PlayOneShot()
        {
            if (Math.Abs(MinPitch - MaxPitch) > 0.01) 
                _source.pitch = Random.Range(MinPitch, MaxPitch);
            
            _source.PlayOneShot(_source.clip);
        }

        public void Stop() => _source.Stop();
    }
}