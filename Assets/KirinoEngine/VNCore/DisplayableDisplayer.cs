using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.Tween;
using UnityEngine;

namespace KirinoEngine {
    public class DisplayableDisplayer : MonoBehaviour {
        public enum TweenType {
            Linear,
            CubicEaseInOut,
            CubicEaseIn,
            CubicEaseOut
        }

        // Enqueue `true` when a drawing coroutine starts,
        // and dequeue when one ends.
        private readonly Queue<bool> currentDrawingRoutine = new Queue<bool>();

        public DisplayableImage displayableImagePrefab;

        // name and tag
        // usage::  Replace exist image (for various faces of same character) require both name and tag
        // name can be same, tag can't be same between images.

        // exampale:
        // first sprite 'kirino angry':: name: kirino, tag: angry
        // second sprite 'kirino happy':: name: kirino, tag: happy

        // situation: 'kirino angry' is alreay displayed
        // => show kirino happy will replace kirino angry

        // Replaceing only happening between images which have same name with diffrent tag
        public Dictionary<string, DisplayableImage> displayableImages = new Dictionary<string, DisplayableImage>();

        [Header("References")] public Transform displayablesHolder;

        [Header("Options")] public float dissolveTime = 0.5f;

        public bool isDrawing
        {
            get
            {
                if (currentDrawingRoutine.Count > 0)
                    return true;
                return false;
            }
        }

        public virtual void Show(Displayable displayable) {
            if (displayable == null)
            {
                Debug.LogError("Show Displayable Error: Null displayable");
            }
            else
            {
                // 만약 이미 같은 이름의 디스플레이어블이 그려져 있다면
                if (displayableImages.ContainsKey(displayable.tag))
                    ReplaceImage(displayable);
                else // 같은 이름의 디스플레이어블이 없다면
                    AddImage(displayable);
            }
        }

        private void AddImage(Displayable displayable) {
            var displayableImage = Instantiate(displayableImagePrefab);
            displayableImage.transform.SetParent(displayablesHolder);
            displayableImage.transform.localScale = Vector3.one;

            displayableImage.Setup(displayable);

            displayableImages.Add(displayable.tag, displayableImage);

            UpdateDisplayablePivot(displayable.tag, PivotPresets.MiddleCenter);
            UpdateDisplayableAnchor(displayable.tag, AnchorPresets.MiddleCenter);
            UpdateDisplayablePosition(displayable.tag, Vector2.zero);

            StartCoroutine("DissolveIn", displayableImage);
        }

        private void ReplaceImage(Displayable displayable) {
            // prevImage is not inserted into images list.
            var prevImage = Instantiate(displayableImages[displayable.tag], displayablesHolder);
            prevImage.name += "_prev";

            //prevImage.transform.position = images[displayable.tag].transform.position;

            // Replace sprite in existing image component.
            displayableImages[displayable.tag].ChangeDisplayable(displayable);

            StartCoroutine(DissolveOutAndDestroy(prevImage, null));
            StartCoroutine(DissolveIn(displayableImages[displayable.tag]));
        }

        public void UpdatePosition(string displayableName, Vector2 anchoredPosition) {
            UpdateDisplayablePosition(displayableName, anchoredPosition);
        }

        public void UpdateAnchor(string displayableName, AnchorPresets anchor) {
            UpdateDisplayableAnchor(displayableName, anchor);
        }

        public void UpdatePivot(string displayableName, PivotPresets pivot) {
            UpdateDisplayablePivot(displayableName, pivot);
        }

        public void UpdateSize(string displayableName, Vector2 size) {
            UpdateDisplayableSize(displayableName, size);
        }

        private void UpdateDisplayablePosition(string displayableName, Vector2 anchoredPosition) {
            if (displayableImages.ContainsKey(displayableName))
            {
                displayableImages[displayableName].imageRect.anchoredPosition = anchoredPosition;
                StartCoroutine(DissolveIn(displayableImages[displayableName]));
            }
            else
            {
                Debug.LogWarning("Updapte Position Error: Can't find " + displayableName);
            }
        }

        private void UpdateDisplayableAnchor(string displayableName, AnchorPresets anchor) {
            if (displayableImages.ContainsKey(displayableName))
            {
                displayableImages[displayableName].imageRect.SetAnchor(anchor);
                StartCoroutine(DissolveIn(displayableImages[displayableName]));
            }
            else
            {
                Debug.LogWarning("Updapte Position Error: Can't find " + displayableName);
            }
        }

        private void UpdateDisplayablePivot(string displayableName, PivotPresets pivot) {
            if (displayableImages.ContainsKey(displayableName))
            {
                displayableImages[displayableName].imageRect.SetPivot(pivot);
                StartCoroutine(DissolveIn(displayableImages[displayableName]));
            }
            else
            {
                Debug.LogWarning("Updapte Position Error: Can't find " + displayableName);
            }
        }

        private void UpdateDisplayableSize(string displayableName, Vector2 newDeltaSize) {
            if (displayableImages.ContainsKey(displayableName))
            {
                displayableImages[displayableName].UpdateSize(newDeltaSize);
                StartCoroutine(DissolveIn(displayableImages[displayableName]));
            }
            else
            {
                Debug.LogWarning("Updapte Position Error: Can't find " + displayableName);
            }
        }

        public void Hide(string displayableTag) {
            try
            {
                DisplayableImage targetImage = null;
                if (!displayableImages.ContainsKey(displayableTag))
                {
                    foreach (var image in displayableImages.Values)
                        if (image.displayableData.name == displayableTag)
                        {
                            targetImage = image;
                            break;
                        }

                    if (targetImage == null) throw new KeyNotFoundException();
                }
                else
                {
                    targetImage = displayableImages[displayableTag];
                }

                displayableImages.Remove(targetImage.displayableData.tag);

                StartCoroutine(DissolveOutAndDestroy(targetImage, () =>
                {
                    //if (!isDrawing) VNController.parser.MoveNext();
                }));
            }
            catch (KeyNotFoundException e)
            {
                throw new Exception("Hide Error : no displayable has tag : " + displayableTag);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            //VNController.parser.MoveNext(); 코루틴에서 처리했으므로 삭제
        }

        public void HideAll() {
            foreach (var displayableName in displayableImages.Keys)
                StartCoroutine(DissolveOutAndDestroy(displayableImages[displayableName], null));

            displayableImages.Clear();

            // VNController.parser.MoveNext();
        }

        public void ScaleDisplayable(string displayableTag, float targetScale, float deltaTime, TweenType tweenType) {
            // displayableTag 변수의 값으로 태그가 아닌 이름을 넣은 경우, 이름이 일치하는 이미지를 찾아, 그것의 태그 값을 가져와 덮어쓰기함
            if (!displayableImages.ContainsKey(displayableTag))
                Debug.LogError("SetScale Error : 해당하는 키가 없음 : " + displayableTag);

            var startSize = displayableImages[displayableTag].transform.localScale;
            var targetSize = Vector3.one * targetScale;

            if (tweenType == TweenType.CubicEaseInOut)
                displayableImages[displayableTag].gameObject.Tween("MoveDisplayableImage", startSize, targetSize,
                    deltaTime, TweenScaleFunctions.CubicEaseInOut, t =>
                    {
                        if (displayableImages.ContainsKey(displayableTag))
                            displayableImages[displayableTag].transform.localScale = t.CurrentValue;
                    });
            else if (tweenType == TweenType.Linear)
                displayableImages[displayableTag].gameObject.Tween("MoveDisplayableImage", startSize, targetSize,
                    deltaTime, TweenScaleFunctions.Linear, t =>
                    {
                        if (displayableImages.ContainsKey(displayableTag))
                            displayableImages[displayableTag].transform.localScale = t.CurrentValue;
                    });
            else if (tweenType == TweenType.CubicEaseIn)
                displayableImages[displayableTag].gameObject.Tween("MoveDisplayableImage", startSize, targetSize,
                    deltaTime, TweenScaleFunctions.CubicEaseIn, t =>
                    {
                        if (displayableImages.ContainsKey(displayableTag))
                            displayableImages[displayableTag].transform.localScale = t.CurrentValue;
                    });
            else if (tweenType == TweenType.CubicEaseOut)
                displayableImages[displayableTag].gameObject.Tween("MoveDisplayableImage", startSize, targetSize,
                    deltaTime, TweenScaleFunctions.CubicEaseOut, t =>
                    {
                        if (displayableImages.ContainsKey(displayableTag))
                            displayableImages[displayableTag].transform.localScale = t.CurrentValue;
                    });
        }

        public void MoveDisplayable(string displayableTag, Vector2 targetPosition, float deltaTime,
            TweenType tweenType) {
            // displayableTag 변수의 값으로 태그가 아닌 이름을 넣은 경우, 이름이 일치하는 이미지를 찾아, 그것의 태그 값을 가져와 덮어쓰기함
            if (!displayableImages.ContainsKey(displayableTag))
                foreach (var displayable in displayableImages.Values)
                    if (displayableTag == displayable.displayableData.name)
                        displayableTag = displayable.displayableData.tag;

            var startPosition = displayableImages[displayableTag].imageRect.anchoredPosition;

            if (tweenType == TweenType.CubicEaseInOut)
                displayableImages[displayableTag].gameObject.Tween("MoveDisplayableImage", startPosition,
                    targetPosition, deltaTime, TweenScaleFunctions.CubicEaseInOut, t =>
                    {
                        if (displayableImages.ContainsKey(displayableTag))
                            displayableImages[displayableTag].imageRect.anchoredPosition = t.CurrentValue;
                    });
            else if (tweenType == TweenType.Linear)
                displayableImages[displayableTag].gameObject.Tween("MoveDisplayableImage", startPosition,
                    targetPosition, deltaTime, TweenScaleFunctions.Linear, t =>
                    {
                        if (displayableImages.ContainsKey(displayableTag))
                            displayableImages[displayableTag].imageRect.anchoredPosition = t.CurrentValue;
                    });
            else if (tweenType == TweenType.CubicEaseIn)
                displayableImages[displayableTag].gameObject.Tween("MoveDisplayableImage", startPosition,
                    targetPosition, deltaTime, TweenScaleFunctions.CubicEaseIn, t =>
                    {
                        if (displayableImages.ContainsKey(displayableTag))
                            displayableImages[displayableTag].imageRect.anchoredPosition = t.CurrentValue;
                    });
            else if (tweenType == TweenType.CubicEaseOut)
                displayableImages[displayableTag].gameObject.Tween("MoveDisplayableImage", startPosition,
                    targetPosition, deltaTime, TweenScaleFunctions.CubicEaseOut, t =>
                    {
                        if (displayableImages.ContainsKey(displayableTag))
                            displayableImages[displayableTag].imageRect.anchoredPosition = t.CurrentValue;
                    });
        }

        private IEnumerator DissolveIn(DisplayableImage displayableImage) {
            currentDrawingRoutine.Enqueue(true);

            var alpha = 0.0f;
            displayableImage.alpha = alpha;

            var startTime = Time.time;

            while (Time.time <= startTime + dissolveTime)
            {
                alpha += Time.deltaTime / dissolveTime;
                displayableImage.alpha = alpha;

                yield return null;
            }

            displayableImage.alpha = 1.0f;

            currentDrawingRoutine.Dequeue();

            //if (!isDrawing) VNController.parser.MoveNext();
        }

        private IEnumerator DissolveOutAndDestroy(DisplayableImage displayableImage, Action callback) {
            currentDrawingRoutine.Enqueue(true);

            var alpha = 1.0f;
            displayableImage.alpha = alpha;

            var startTime = Time.time;

            while (Time.time <= startTime + dissolveTime)
            {
                alpha -= Time.deltaTime / dissolveTime;
                displayableImage.alpha = alpha;

                yield return null;
            }

            currentDrawingRoutine.Dequeue();
            Destroy(displayableImage.gameObject);

            if (callback != null) callback();
        }
    }
}