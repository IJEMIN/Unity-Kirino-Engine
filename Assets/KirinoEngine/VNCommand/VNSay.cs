using UnityEngine;
using System.Collections;

namespace KirinoEngine
{
    public class VNSay : VNCommand
    {

        public string characterName;
        public string dialogue;

        public VNSay(string characterName_, string dialogue_)
        {
            characterName = characterName_;
            dialogue = dialogue_;
        }
        public VNSay(string dialogue_)
        {
            characterName = string.Empty;
            dialogue = dialogue_;
        }
        public override void Invoke()
        {
            VNController.textDisplayer.SetSay(characterName, dialogue);
        }
    }
}
