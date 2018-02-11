using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KirinoEngine
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource musicPlayer;
        public AudioSource sfxPlayer;
        public AudioSource voicePlayer;

        //clip for test
        public AudioClip debugSound;

        void OnEnable()
        {
            if(!musicPlayer || !sfxPlayer || !voicePlayer)
            {
                Debug.LogWarning("AudioSource for AudioManager is not assigned!");
            }
        }

        public void StopMusic()
        {
            musicPlayer.Stop();
        }

        public void PlayMusic(AudioClip music)
        {
            musicPlayer.clip = music;
            musicPlayer.Play();
        }

        public void PlaySFX(AudioClip sfx)
        {
            sfxPlayer.PlayOneShot(sfx);
        }

        public void PlayVoice(AudioClip voice)
        {
            voicePlayer.clip = voice;
            voicePlayer.Play();
        }

        public void ToggleVoice(bool active_)
        {
            voicePlayer.mute = !active_;
        }
        public void ToggleMusic(bool active_)
        {
            musicPlayer.mute = !active_;
        }
    }
}


