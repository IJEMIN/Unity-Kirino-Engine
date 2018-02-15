using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    public class VNShow : VNCommand
    {

        public Displayable displayable;

        public VNShow(Displayable displayable_)
        {
            displayable = displayable_;
        }
        public override void Invoke()
        {
            Debug.Log(displayable.name);
            Debug.Log(displayable.tag);
            VNController.displayableDisplayer.Show(displayable);

        }
    }
}