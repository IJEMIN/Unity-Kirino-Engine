using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    public class VNMusicPlay : VNCommand
    {
        AudioClip clip;

        public VNMusicPlay(AudioClip _clip)
        {
            clip = _clip;
        }

        public override void Invoke()
        {
            VNController.audioManager.PlayMusic(clip);
        }
    }
}
