using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    public class VNSwitch : VNCommand
    {
        public string SwitchName;
        public VNSwitch(string SwitchName_)
        {
            SwitchName = SwitchName_;
        }
        public override void Invoke()
        {
            VNController.gameSwitch.SwitchMaker(SwitchName);
            Debug.Log(SwitchName);
        }
    }
}
