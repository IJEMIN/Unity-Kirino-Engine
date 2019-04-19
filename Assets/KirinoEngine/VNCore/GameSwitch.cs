using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KirinoEngine
{
    public class GameSwitch : MonoBehaviour
    {
        public string SwitchGoto;
        public static Dictionary<string, bool> Switch = new Dictionary<string, bool>();
        public void SwitchCommander(string SwitchName_, string SwitchGoto_)
        {
            if(SwitchGoto_ != string.Empty)
            {
                if(GameSwitch.Switch.ContainsKey(SwitchName_))
                {
                    SwitchGoto = SwitchGoto_;
                    SceneManager.LoadScene(SwitchGoto);
                }
                else
                {
                    Debug.Log(string.Format("{0} is Not On",SwitchName_));
                }
            }
            else
            {
                if (GameSwitch.Switch.ContainsKey(SwitchName_))
                {
                    Switch.Remove(SwitchName_);
                    Debug.Log(string.Format("[{0}] Switch Off",SwitchName_));
                }
                else
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
        }
    }
}


   
