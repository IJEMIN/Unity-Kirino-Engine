using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace KirinoEngine
{
    public class VNLoadScene : VNCommand
    {
        string sceneName;

        public VNLoadScene(string sceneName_)
        {
            sceneName = sceneName_;
        }

        public override void Invoke()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
