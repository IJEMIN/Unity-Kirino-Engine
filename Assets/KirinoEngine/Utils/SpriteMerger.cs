using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


// Merge multiple Sprites as one sprite
public static class SpriteMerger
{
    [System.Serializable]
    public struct Coordinate
    {
        public int x;
        public int y;
    }

    [System.Serializable]
    public class SpriteMapper
    {
        public Sprite sprite;
        public Coordinate offset;
    }
    public static Sprite MergeSprite(Sprite[] sprites)
    {
        // 스프라이트 중 가장 큰 크기의 것으로 지정되야함
        int width = 0;
        int height = 0;

        foreach(var sprite in sprites)
        {
            width = Mathf.Max(width,sprite.texture.width);
            height = Mathf.Max(height,sprite.texture.height);
        }

        Texture2D mergedTexture = new Texture2D(width,height);
        mergedTexture.alphaIsTransparency = true;

        for (int i = 0; i <width; i ++)
        {
            for (int j = 0; j < height; j++)
            {
                mergedTexture.SetPixel(i,j,Color.clear);
            }
        }

        // overdrawn by last one
        foreach (var sprite in sprites)
        {
            for(int y = 0; y< sprite.texture.height; y++)
            {
                for (int x = 0; x< sprite.texture.width; x++)
                {
                    Color extractedPixel = sprite.texture.GetPixel(x,y);

                    Color replacedColor = mergedTexture.GetPixel(x,y);

                    replacedColor = (1.0f - extractedPixel.a) * replacedColor + extractedPixel.a * extractedPixel;
                
                    mergedTexture.SetPixel(x,y,replacedColor);
                }
            }

            mergedTexture.Apply();
        }

      

        Sprite mergedSprite = Sprite.Create(mergedTexture,new Rect(0,0,width,height),new Vector2(0.5f,0.5f));

        return mergedSprite;
    }

    public static Sprite MergeSprite(SpriteMapper[] spritesMap)
    {
        // 스프라이트 중 가장 큰 크기의 것으로 지정되야함
        int width = 0;
        int height = 0;

        foreach(var mapper in spritesMap)
        {
            Sprite sprite = mapper.sprite;

            width = Mathf.Max(width,sprite.texture.width);
            height = Mathf.Max(height,sprite.texture.height);
        }

        Texture2D mergedTexture = new Texture2D(width,height);

        for (int i = 0; i <width; i ++)
        {
            for (int j = 0; j < height; j++)
            {
                mergedTexture.SetPixel(i,j,Color.clear);
            }
        }

        mergedTexture.alphaIsTransparency = true;

        // overdrawn by last one
        foreach(var mapper in spritesMap)
        {
            Sprite sprite = mapper.sprite;

            for(int y = 0; y< sprite.texture.height; y++)
            {
                for (int x = 0; x< sprite.texture.width; x++)
                {
                    Color extractedPixel = sprite.texture.GetPixel(x,y);

                    Color replacedColor = mergedTexture.GetPixel(x + mapper.offset.x,y + mapper.offset.y);

                    replacedColor = (1.0f - extractedPixel.a) * replacedColor + extractedPixel.a * extractedPixel;
                
                    mergedTexture.SetPixel(x + mapper.offset.x,y + mapper.offset.y,replacedColor);
                }
            }

            mergedTexture.Apply();
        }

      

        Sprite mergedSprite = Sprite.Create(mergedTexture,new Rect(0,0,width,height),new Vector2(0.5f,0.5f));
        
        return mergedSprite;
    }
}