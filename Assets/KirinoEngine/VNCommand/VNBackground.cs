using UnityEngine;
using System.Collections;

namespace KirinoEngine
{
    public class VNBackground : VNCommand
    {

        public Sprite sprite;

        public VNBackground(Sprite sprite)
        {
            this.sprite = sprite;
        }
        public override void Invoke()
        {
            VNController.backgroundDisplayer.ChangeBackground(sprite);
        }
    }
}