using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KirinoEngine
{
    public class GameSwitch : MonoBehaviour
    {
        public string SwitchGoto;
        public static Dictionary<string, bool> Switch = new Dictionary<string, bool>();
        public void SwitchCommander(string SwitchName_, bool SwitchStats_)
        {
            if (GameSwitch.Switch.ContainsKey(SwitchName_) && !SwitchStats_)
            {
                Switch.Remove(SwitchName_);
                Debug.Log(string.Format("[{0}] Switch Off", SwitchName_));
            }
            else if (!GameSwitch.Switch.ContainsKey(SwitchName_) && SwitchStats_)
            {
                try
                {
                    Switch.Add(SwitchName_, true);
                    Debug.Log(string.Format("[{0}] Switch ON", SwitchName_));
                }
                catch (System.ArgumentException)
                {
                    Debug.Log("Error");
                }
            }
        }
        public void SwitchCommander(string SwitchName_, string SwitchGoto_)
        {
            if (GameSwitch.Switch.ContainsKey(SwitchName_))
            {
                SceneManager.LoadScene(SwitchGoto_);
            }
            else
            {
                Debug.Log(string.Format("[{0}] Switch Not ON (Skipped)", SwitchName_));
            }
        }
    }
}
