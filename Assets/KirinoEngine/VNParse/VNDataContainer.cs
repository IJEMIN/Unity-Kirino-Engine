using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace KirinoEngine
{
    public class VNDataController: MonoBehaviour
    {
        public static VNDataController Instance{
            get{
                return FindObjectOfType<VNDataController>();
            }
        }

        public List<Displayable> displayables;
        public List<AudioClip> audioClips;

        public Displayable GetDisplayable(string key)
        {
            foreach (Displayable displayable in displayables)
            {
                if (displayable.key == key)
                {
                    return displayable;
                }
            }

            Debug.LogWarning(string.Format("There is no {0} displayble", key));
            return null;
        }

        public AudioClip GetAudioClip(string clipName)
        {
            foreach (AudioClip clip in audioClips)
            {
                if (clip.name == clipName)
                {
                    return clip;
                }
            }

			Debug.LogWarning(string.Format("There is no {0} clip", clipName));
			return null;
        }
    }

}
