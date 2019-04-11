using UnityEngine;

namespace KirinoEngine {
    public class AudioManager : MonoBehaviour {
        public AudioSource musicPlayer;
        public AudioSource sfxPlayer;
        public AudioSource voicePlayer;

        public float musicVolume
        {
            get
            {
                musicPlayer.volume = PlayerPrefs.GetFloat("Music Volume", 1.0f);
                return musicPlayer.volume;
            }
            set
            {
                PlayerPrefs.SetFloat("Music Volume", value);
                musicPlayer.volume = value;
            }
        }

        public float voiceVolume
        {
            get
            {
                voicePlayer.volume = PlayerPrefs.GetFloat("Voice Volume", 1.0f);
                return voicePlayer.volume;
            }
            set
            {
                PlayerPrefs.SetFloat("Voice Volume", value);
                voicePlayer.volume = value;
            }
        }

        public float sfxVolume
        {
            get
            {
                sfxPlayer.volume = PlayerPrefs.GetFloat("SFX Volume", 1.0f);
                return sfxPlayer.volume;
            }
            set
            {
                PlayerPrefs.SetFloat("SFX Volume", value);
                sfxPlayer.volume = value;
            }
        }


        public bool musicMute
        {
            get
            {
                var muteInt = PlayerPrefs.GetInt("Music Mute", 0);
                var mute = muteInt == 1 ? true : false;

                musicPlayer.mute = mute;

                return mute;
            }

            set
            {
                musicPlayer.mute = value;
                var muteInt = value ? 1 : 0;
                PlayerPrefs.SetInt("Music Mute", muteInt);
            }
        }

        public bool voiceMute
        {
            get
            {
                var muteInt = PlayerPrefs.GetInt("Voice Mute", 0);
                var mute = muteInt == 1 ? true : false;

                voicePlayer.mute = mute;

                return mute;
            }

            set
            {
                voicePlayer.mute = value;
                var muteInt = value ? 1 : 0;
                PlayerPrefs.SetInt("Voice Mute", muteInt);
            }
        }

        public bool sfxMute
        {
            get
            {
                var muteInt = PlayerPrefs.GetInt("SFX Mute", 0);
                var mute = muteInt == 1 ? true : false;

                sfxPlayer.mute = mute;

                return mute;
            }

            set
            {
                sfxPlayer.mute = value;
                var muteInt = value ? 1 : 0;
                PlayerPrefs.SetInt("SFX Mute", muteInt);
            }
        }

        private void Awake() {
            if (!musicPlayer || !sfxPlayer || !voicePlayer)
            {
                Debug.LogWarning("AudioSource for AudioManager is not assigned!");
            }
            else
            {
                musicPlayer.mute = musicMute;
                voicePlayer.mute = voiceMute;
                sfxPlayer.mute = sfxMute;

                musicPlayer.volume = musicVolume;
                voicePlayer.volume = voiceVolume;
                sfxPlayer.volume = sfxVolume;
            }


            musicPlayer.loop = true;
            sfxPlayer.loop = false;
            voicePlayer.loop = false;
        }

        public void StopMusic() {
            musicPlayer.Stop();
        }

        public void PlayMusic(AudioClip music) {
            if (music != musicPlayer.clip) musicPlayer.clip = music;

            if (!musicPlayer.isPlaying) musicPlayer.Play();
        }

        public void PlaySoundEffect(AudioClip sfx) {
            sfxPlayer.clip = sfx;
            sfxPlayer.Play();
        }

        public void PlayVoice(AudioClip voice) {
            voicePlayer.clip = voice;
            voicePlayer.Play();
        }

    }
}