using UnityEngine;
using System.Collections;

namespace KirinoEngine
{
    public class VNBackground : VNCommand
    {

        public Displayable displayable;

        public VNBackground(Displayable displayable_)
        {
            displayable = displayable_;
        }
        public override void Invoke()
        {
            VNController.backgroundDisplayable.ChangeBackground(displayable.sprite);
        }
    }
}