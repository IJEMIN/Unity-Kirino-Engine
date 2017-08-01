using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MyExtensions;

namespace KirinoEngine
{
    using VNCore;

    public class BackgroundDisplayer : MonoBehaviour
    {

        public float dissolveTime = 0.5f;
        Image m_backgroundDisplayer;

        // Enqueue `true` when a drawing coroutine starts,
        // and dequeue when one ends.
        private Queue<bool> currentDrawingRoutine = new Queue<bool>();


        public bool isChanging
        {
            get;
            private set;
        }

        void Awake()
        {
            m_backgroundDisplayer = GetComponent<Image>();
        }

        public void ChangeBackground(Sprite newBackground)
        {
            VNLocator.displayableDisplayer.HideAll();
            VNLocator.textDisplayer.HideDialogueHolder();


            var prevImage = Instantiate(m_backgroundDisplayer, m_backgroundDisplayer.transform.parent);
            prevImage.name += "_prev";

            m_backgroundDisplayer.sprite = newBackground;

            StartCoroutine("DissolveOutAndDestroy", prevImage);
            StartCoroutine("DissolveIn", m_backgroundDisplayer);
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