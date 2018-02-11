using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    public class VNVoice : VNCommand {

        AudioClip clip;

        public VNVoice(string clipName)
        {
            clip = VNDataController.Instance.GetAudioClip(clipName);
        }

        public override void Invoke()
        {
            VNController.audioManager.PlayVoice(clip);
        }
    }

}