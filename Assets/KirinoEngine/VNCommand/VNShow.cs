using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    using VNCore;

    public class VNShow : VNCommand
    {

        public Displayable displayable;

        public VNShow(Displayable displayable_)
        {
            displayable = displayable_;
        }
        public override void Invoke()
        {
            VNLocator.displayableDisplayer.Show(displayable);
            VNLocator.currentEpisode.InvokeNextCommand();
        }
    }
}