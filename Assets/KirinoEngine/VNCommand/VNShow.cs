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
            // Show base body
            if (displayable.sprite.name.Contains("front"))
            {
                var bodyFront = VNDataController.Instance.GetDisplayable("body_front");
                VNController.displayableDisplayer.Show(bodyFront);
            }
            else if (displayable.sprite.name.Contains("side"))
            {
                var bodySide = VNDataController.Instance.GetDisplayable("body_side");
                VNController.displayableDisplayer.Show(bodySide);
            }

            /* 잠시 표정 출력을 막기위해 */
           // VNLocator.displayableDisplayer.Show(displayable);
        }
    }
}