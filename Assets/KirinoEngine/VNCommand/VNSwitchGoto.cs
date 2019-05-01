using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KirinoEngine
{
    public class VNSwitchGoto : VNCommand
    {
        public string SwitchName;
        public string SwitchGoto;
        public VNSwitchGoto(string SwitchName_, string SwitchGoto_)
        {
            SwitchGoto = SwitchGoto_;
            SwitchName = SwitchName_;
        }
        public override void Invoke()
        {
            VNController.gameSwitch.SwitchCommander(SwitchName, SwitchGoto);
        }
    }
}
