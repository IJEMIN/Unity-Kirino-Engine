using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KirinoEngine
{
    [System.Serializable]
    public class Displayable
    {
        // name can be Duplicated
        public string name;

        // diffrent tag with same name will replace older sprite
        public string tag;
        public SpriteMerger.SpriteMapper[] sprites;

        private Sprite m_mergedSprite = null;

        public Sprite mergedSprite{
            get{
                if(m_mergedSprite == null)
                {
                    m_mergedSprite = SpriteMerger.MergeSprite(sprites);
                }
                return m_mergedSprite;
            }
        }

        public Vector2 size
        {
            get{
                return new Vector2(mergedSprite.texture.width,mergedSprite.texture.height);
            }
        }
    }
}
