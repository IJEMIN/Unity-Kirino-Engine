using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    [CreateAssetMenu(fileName = "displayable", menuName = "Empty Displayable", order = 1)]
    public class Displayable : ScriptableObject
    {
        // key can't be duplicated
        public string key { get { return name; } }

        // tag can be same throught many displayable
        public string tag;

        public Sprite sprite;

        public Vector2 size;
    }
}
