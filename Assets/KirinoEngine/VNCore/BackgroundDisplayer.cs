using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KirinoEngine {
    public class BackgroundDisplayer : MonoBehaviour {
        // Enqueue `true` when a drawing coroutine starts,
        // and dequeue when one ends.
        private readonly Queue<bool> currentDrawingRoutine = new Queue<bool>();

        public float dissolveTime = 0.5f;
        public Image m_backgroundDisplayer;

        public Sprite CurrentSprite => m_backgroundDisplayer.sprite;


        public bool IsDrawing => currentDrawingRoutine.Count > 0;

        protected virtual void Awake() {
            m_backgroundDisplayer = GetComponent<Image>();
        }

        public void ChangeBackground(Sprite newBackground) {
            VNController.displayableDisplayer.HideAll();
            VNController.textDisplayer.HideDialogueHolder();

            UpdateBackgroundSprite(newBackground,null);
        }

        public void UpdateBackgroundSprite(Sprite newSprite, Action onDissolveEnd) {
            var prevImage = Instantiate(m_backgroundDisplayer, m_backgroundDisplayer.transform);
            prevImage.name += "_prev";

            m_backgroundDisplayer.sprite = newSprite;

            StartCoroutine(DissolveOutAndDestroy(prevImage));
            StartCoroutine(DissolveIn(m_backgroundDisplayer, onDissolveEnd));
        }

        private IEnumerator DissolveIn(Image image, Action onDissolveEnd) {
            currentDrawingRoutine.Enqueue(true);

            var alpha = 0.0f;
            image.SetTransparency(alpha);

            var startTime = Time.time;

            while (Time.time <= startTime + dissolveTime)
            {
                alpha += Time.deltaTime / dissolveTime;
                image.SetTransparency(alpha);

                yield return null;
            }

            image.SetTransparency(1.0f);

            currentDrawingRoutine.Dequeue();

            onDissolveEnd?.Invoke();
        }

        private IEnumerator DissolveOutAndDestroy(Image image) {
            currentDrawingRoutine.Enqueue(true);

            var alpha = 1.0f;
            image.SetTransparency(alpha);

            var startTime = Time.time;

            while (Time.time <= startTime + dissolveTime)
            {
                alpha -= Time.deltaTime / dissolveTime;
                image.SetTransparency(alpha);

                yield return null;
            }

            currentDrawingRoutine.Dequeue();

            Destroy(image.gameObject);
        }
    }
}