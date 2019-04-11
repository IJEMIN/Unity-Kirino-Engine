using System;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine {
    [Serializable]
    public class SpriteMapper {
        //Left-Top Anchor and Pivot
        public Vector2 pos = new Vector2(0, 0);

        public Vector2 size = new Vector2(100, 100);
        public Sprite sprite;
    }

    public class Displayable {
        public Vector2 canvasSize;

        // name can be Duplicated
        public string name;
        public Vector2 offset;

        public List<SpriteMapper> spriteMappers = new List<SpriteMapper>();

        // diffrent tag with same name will replace older sprite
        public string tag;


        public void Resize(float percentage) {
            canvasSize *= percentage;

            offset *= percentage;

            foreach (var spriteMap in spriteMappers)
            {
                spriteMap.pos *= percentage;
                spriteMap.size *= percentage;
            }
        }
    }
}