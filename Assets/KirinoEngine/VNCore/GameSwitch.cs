using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    public class GameSwitch : MonoBehaviour
    {
        public Dictionary<string, bool> Switch = new Dictionary<string, bool>();
        public void SwitchMaker(string SwitchName_)
        {
            if (!Switch[SwitchName_])
            {
                Switch.Add(SwitchName_, true);
                Debug.Log(SwitchName_ + "생성");
            }
            else if (Switch[SwitchName_])
            {
                Switch.Remove(SwitchName_);
                Debug.Log(SwitchName_ + "삭제");
            }
        }
    }
}

   
