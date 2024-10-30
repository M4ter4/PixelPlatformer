using UnityEngine;

namespace UI
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance{ get; private set; }
        private AudioSource _audioSource;
        void Start()
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = PlayerPrefs.GetFloat("volume", 1f);
        }
    
        public void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        public void ChangeVolume(float volume)
        {
            float currentVolume = PlayerPrefs.GetFloat("volume", 1f);
            currentVolume += volume;
            if(currentVolume > 1f)
                currentVolume = 0f;
            _audioSource.volume = currentVolume;
            PlayerPrefs.SetFloat("volume", currentVolume);
        }
    }
}
