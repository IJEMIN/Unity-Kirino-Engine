using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace KirinoEngine
{
    public class DisplayableDisplayer : MonoBehaviour
    {
        

        [Header("Options")]
        public float dissolveTime = 0.5f;

        // Enqueue `true` when a drawing coroutine starts,
        // and dequeue when one ends.
        private Queue<bool> currentDrawingRoutine = new Queue<bool>();

        [Header("References")]
        public Transform displayablesHolder;

        // name and tag
        // usage::  Replace exist image (for various faces of same character) require both name and tag
        // name can be same, tag can't be same between images.

        // exampale:
        // first sprite 'kirino angry':: name: kirino, tag: angry
        // second sprite 'kirino happy':: name: kirino, tag: happy

        // situation: 'kirino angry' is alreay displayed
        // => show kirino happy will replace kirino angry

        // Replaceing only happening between images which have same name with diffrent tag
        public Dictionary<string, Image> images = new Dictionary<string, Image>();

        public void Show(Displayable displayable)
        {
            // 만약 이미 같은 이름의 디스플레이어블이 그려져 있다면
            if(images.ContainsKey(displayable.name))
            {
                ReplaceImage(displayable);
            }
            else // 같은 이름의 디스플레이어블이 없다면
            {
                AddImage(displayable);
            }
        }

        private void AddImage(Displayable displayable)
        {
            var image = new GameObject(displayable.name).AddComponent<Image>();
            image.sprite = displayable.mergedSprite;

            image.transform.SetParent(displayablesHolder);

            image.transform.localScale = Vector3.one;
            image.rectTransform.SetSize(displayable.size);

            image.rectTransform.anchoredPosition = Vector2.zero;
            image.preserveAspect = true;

            images.Add(displayable.name, image);

            StartCoroutine("DissolveIn", image);
        }

        private void ReplaceImage(Displayable displayable)
        {
            // prevImage is not inserted into images list.
            var prevImage = Instantiate(images[displayable.name], displayablesHolder);
            prevImage.name += "_prev";

            // Replace sprite in existing image component.
            images[displayable.name].sprite = displayable.mergedSprite;

            StartCoroutine("DissolveOutAndDestroy", prevImage);
            StartCoroutine("DissolveIn", images[displayable.name]);
        }

        public void Hide(string displayableName)
        {
            var targetImage = images[displayableName].GetComponent<Image>();
            images.Remove(displayableName);

            StartCoroutine("DissolveOutAndDestroy", targetImage);
        }

        public void HideAll()
        {
            foreach (var displayableName in images.Keys)
            {
                StartCoroutine("DissolveOutAndDestroy", images[displayableName]);
            }

            images.Clear();
        }



        IEnumerator DissolveIn(Image image)
        {
            currentDrawingRoutine.Enqueue(true);

            float alpha = 0.0f;
            image.SetTransparency(alpha);

            float startTime = Time.time;

            while (Time.time <= startTime + dissolveTime)
            {
                alpha += (Time.deltaTime / dissolveTime);
                image.SetTransparency(alpha);

                yield return null;
            }

            image.SetTransparency(1.0f);

            currentDrawingRoutine.Dequeue();
        }

        IEnumerator DissolveOutAndDestroy(Image image)
        {
            currentDrawingRoutine.Enqueue(true);

            float alpha = 1.0f;
            image.SetTransparency(alpha);

            float startTime = Time.time;

            while (Time.time <= startTime + dissolveTime)
            {
                alpha -= (Time.deltaTime / dissolveTime);
                image.SetTransparency(alpha);

                yield return null;
            }

            currentDrawingRoutine.Dequeue();

            Destroy(image.gameObject);
        }
    }
}