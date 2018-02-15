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

        void Awake()
        {
            foreach(var displayable in displayables)
            {
                // 실시간으로 머지하면 성능 소모가 심하니 미리 하기
                Debug.Log(displayable.mergedSprite);
            }
        }

        public Displayable GetDisplayable(string displayableName, string displayableTag)
        {
            foreach (Displayable displayable in displayables)
            {
                if (displayable.name == displayableName && displayable.tag == displayableTag)
                {
                    return displayable;
                }
            }

            Debug.LogWarning(string.Format("{0} {1} displayble wasn't imported", name,tag));
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
