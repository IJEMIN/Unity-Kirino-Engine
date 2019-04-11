using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KirinoEngine {
    [RequireComponent(typeof(CanvasGroup))]
    public class DisplayableImage : MonoBehaviour {
        public CanvasGroup canvasGroup;

        private readonly List<RectTransform> childRects = new List<RectTransform>();

        public RectTransform imageRect { get; private set; }

        public Displayable displayableData { get; private set; }

        public float alpha
        {
            get { return canvasGroup.alpha; }

            set { canvasGroup.alpha = value; }
        }

        private void Awake() {
            imageRect = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Setup(Displayable newData) {
            displayableData = newData;

            name = displayableData.name;

            imageRect.SetPivotAndAnchors(new Vector2(0.5f, 0.5f));
            imageRect.SetSize(newData.canvasSize);

            var spriteMappers = displayableData.spriteMappers;

            foreach (var spriteMap in spriteMappers)
            {
                var imageInstance = new GameObject(spriteMap.sprite.name).AddComponent<Image>();


                imageInstance.transform.SetParent(transform);

                imageInstance.transform.localScale = Vector3.one;
                imageInstance.sprite = spriteMap.sprite;

                // LEFT TOP
                imageInstance.rectTransform.SetPivot(PivotPresets.TopLeft);
                imageInstance.rectTransform.SetAnchor(AnchorPresets.BottomLeft,
                    spriteMap.pos.x + displayableData.offset.x,
                    displayableData.canvasSize.y - spriteMap.pos.y + displayableData.offset.y);
                imageInstance.rectTransform.SetSize(spriteMap.size);

                childRects.Add(imageInstance.rectTransform);

                // 마지막으로, 홀더의 크기에 자동 스트레치 되도록 변경
                //imageInstance.rectTransform.SetAnchor(AnchorPresets.StretchAll);
                //imageInstance.anchor
            }
        }

        public void ChangeDisplayable(Displayable newData) {
            // Clear
            while (childRects.Count > 0)
            {
                var childRect = childRects[0];

                childRects.RemoveAt(0);

                Destroy(childRect.gameObject);
            }

            Setup(newData);

            //size = oldSize;
        }

        public void UpdateSize(Vector2 newSize) {
            var percentageVec = new Vector2(newSize.x / imageRect.rect.x, newSize.y / imageRect.rect.y);

            imageRect.SetSize(newSize);

            foreach (var childRect in childRects)
            {
                var oldSize = childRect.GetSize();

                var childNewSize = new Vector2(oldSize.x * percentageVec.x, oldSize.y * percentageVec.y);

                childRect.SetSize(childNewSize);

                childRect.SetAnchor(AnchorPresets.BottomLeft, childRect.anchoredPosition.x * percentageVec.x,
                    childRect.anchoredPosition.y * percentageVec.y);
            }
        }
    }
}