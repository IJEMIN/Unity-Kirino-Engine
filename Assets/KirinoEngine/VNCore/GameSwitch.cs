using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    public class GameSwitch : MonoBehaviour
    {
        public Dictionary<string, bool> Switch = new Dictionary<string, bool>();
        public void SwitchMaker(string SwitchName_)
        {
            try
            {
                Switch.Add(SwitchName_, true);
                Debug.Log(SwitchName_ + "생성됨");
            }
            catch(System.ArgumentException)
            {
                Debug.Log("그런키없음");
            }
        }
    }
}


   
