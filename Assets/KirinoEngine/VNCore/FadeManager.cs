using System.Collections;
using UnityEngine;

namespace KirinoEngine {
    public class FadeManager : MonoBehaviour {
        public CanvasGroup fadeHolder;

        public float fadeTime = 1.0f;

        public bool isFading { get; private set; }


        public void FadeIn() {
            StartCoroutine("BlackFadeIn");
        }

        public void FadeOut() {
            StartCoroutine("BlackFadeOut");
        }


        private IEnumerator BlackFadeIn() {
            fadeHolder.blocksRaycasts = true;


            StopCoroutine("BlackFadeOut");

            fadeHolder.alpha = 1.0f;

            var currentTime = Time.time;

            while (currentTime + fadeTime > Time.time)
            {
                fadeHolder.alpha -= Time.deltaTime / fadeTime;


                yield return null;
            }

            fadeHolder.alpha = 0.0f;
            fadeHolder.blocksRaycasts = false;
        }

        private IEnumerator BlackFadeOut() {
            fadeHolder.blocksRaycasts = true;

            StopCoroutine("BlackFadeIn");


            fadeHolder.alpha = 0.0f;

            var currentTime = Time.time;

            while (currentTime + fadeTime > Time.time)
            {
                fadeHolder.alpha += Time.deltaTime / fadeTime;
                yield return null;
            }

            fadeHolder.alpha = 1.0f;
            fadeHolder.blocksRaycasts = false;
        }
    }
}