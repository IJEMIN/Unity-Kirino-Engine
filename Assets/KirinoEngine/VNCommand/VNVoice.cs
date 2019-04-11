using UnityEngine;

namespace KirinoEngine {
    public class VNVoice : VNCommand {
        private readonly AudioClip clip;

        public VNVoice(AudioClip newClip) {
            clip = newClip;
        }

        public override void Invoke() {
            VNController.audioManager.PlayVoice(clip);
        }
    }
}