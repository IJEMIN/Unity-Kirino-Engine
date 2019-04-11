using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    public class VNAudio : VNCommand
	{
        AudioClip clip;

        public VNAudio(AudioClip newClip)
        {
            clip = newClip;
        }

        public override void Invoke()
        {
            VNController.audioManager.PlaySoundEffect(clip);
        }
	}
}
