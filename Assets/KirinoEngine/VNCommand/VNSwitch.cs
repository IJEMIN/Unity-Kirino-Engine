using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    public class VNSwitch : VNCommand
    {
        public string SwitchName;
        public string SwitchGoto;
        public VNSwitch(string SwitchName_)
        {
            SwitchGoto = string.Empty;
            SwitchName = SwitchName_;
        }
        public VNSwitch(string SwitchName_, string SwitchGoto_)
        {
            SwitchGoto = SwitchGoto_;
            SwitchName = SwitchName_;
        }
        public override void Invoke()
        {
            VNController.gameSwitch.SwitchCommander(SwitchName,SwitchGoto);
            //Debug.Log(SwitchName);
        }
    }
}
