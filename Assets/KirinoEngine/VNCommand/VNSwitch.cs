using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    public class VNSwitch : VNCommand
    {
        public string SwitchName;
        public bool SwitchStats;
        public VNSwitch(string SwitchName_, bool SwitchStats_)
        {
            SwitchStats = SwitchStats_;
            SwitchName = SwitchName_;
        }
        public override void Invoke()
        {
            VNController.gameSwitch.SwitchCommander(SwitchName,SwitchStats);
            //Debug.Log(SwitchName);
        }
    }
}
