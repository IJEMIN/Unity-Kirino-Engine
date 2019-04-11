using UnityEngine;

/// 앵커 프리셋을 위해 새로 추가함
public enum AnchorPresets {
    TopLeft,
    TopCenter,
    TopRight,

    MiddleLeft,
    MiddleCenter,
    MiddleRight,

    BottomLeft,
    BottonCenter,
    BottomRight,

    VertStretchLeft,
    VertStretchRight,
    VertStretchCenter,

    HorStretchTop,
    HorStretchMiddle,
    HorStretchBottom,

    StretchAll
}

public enum PivotPresets {
    TopLeft,
    TopCenter,
    TopRight,

    MiddleLeft,
    MiddleCenter,
    MiddleRight,

    BottomLeft,
    BottomCenter,
    BottomRight
}

public static class RectTransformExtensions {
    public static void SetDefaultScale(this RectTransform trans) {
        trans.localScale = Vector3.one;
    }

    public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec) {
        trans.pivot = aVec;
        trans.anchorMin = aVec;
        trans.anchorMax = aVec;
    }

    public static Vector2 GetSize(this RectTransform trans) {
        return trans.rect.size;
    }

    public static float GetWidth(this RectTransform trans) {
        return trans.rect.width;
    }

    public static float GetHeight(this RectTransform trans) {
        return trans.rect.height;
    }

    public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos) {
        trans.localPosition = new Vector3(newPos.x, newPos.y, trans.localPosition.z);
    }

    public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos) {
        trans.localPosition = new Vector3(newPos.x + trans.pivot.x * trans.rect.width,
            newPos.y + trans.pivot.y * trans.rect.height, trans.localPosition.z);
    }

    public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos) {
        trans.localPosition = new Vector3(newPos.x + trans.pivot.x * trans.rect.width,
            newPos.y - (1f - trans.pivot.y) * trans.rect.height, trans.localPosition.z);
    }

    public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos) {
        trans.localPosition = new Vector3(newPos.x - (1f - trans.pivot.x) * trans.rect.width,
            newPos.y + trans.pivot.y * trans.rect.height, trans.localPosition.z);
    }

    public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos) {
        trans.localPosition = new Vector3(newPos.x - (1f - trans.pivot.x) * trans.rect.width,
            newPos.y - (1f - trans.pivot.y) * trans.rect.height, trans.localPosition.z);
    }

    public static void SetSize(this RectTransform trans, Vector2 newSize) {
        var oldSize = trans.rect.size;
        var deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax +
                          new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }

    public static void SetWidth(this RectTransform trans, float newSize) {
        SetSize(trans, new Vector2(newSize, trans.rect.size.y));
    }

    public static void SetHeight(this RectTransform trans, float newSize) {
        SetSize(trans, new Vector2(trans.rect.size.x, newSize));
    }

    // For Setting Anchored Preset
    public static void SetAnchor(this RectTransform source, AnchorPresets allign, float offsetX = 0,
        float offsetY = 0) {
        source.anchoredPosition = new Vector2(offsetX, offsetY);

        switch (allign)
        {
            case AnchorPresets.TopLeft:
            {
                source.anchorMin = new Vector2(0, 1);
                source.anchorMax = new Vector2(0, 1);
                break;
            }
            case AnchorPresets.TopCenter:
            {
                source.anchorMin = new Vector2(0.5f, 1);
                source.anchorMax = new Vector2(0.5f, 1);
                break;
            }
            case AnchorPresets.TopRight:
            {
                source.anchorMin = new Vector2(1, 1);
                source.anchorMax = new Vector2(1, 1);
                break;
            }

            case AnchorPresets.MiddleLeft:
            {
                source.anchorMin = new Vector2(0, 0.5f);
                source.anchorMax = new Vector2(0, 0.5f);
                break;
            }
            case AnchorPresets.MiddleCenter:
            {
                source.anchorMin = new Vector2(0.5f, 0.5f);
                source.anchorMax = new Vector2(0.5f, 0.5f);
                break;
            }
            case AnchorPresets.MiddleRight:
            {
                source.anchorMin = new Vector2(1, 0.5f);
                source.anchorMax = new Vector2(1, 0.5f);
                break;
            }

            case AnchorPresets.BottomLeft:
            {
                source.anchorMin = new Vector2(0, 0);
                source.anchorMax = new Vector2(0, 0);
                break;
            }
            case AnchorPresets.BottonCenter:
            {
                source.anchorMin = new Vector2(0.5f, 0);
                source.anchorMax = new Vector2(0.5f, 0);
                break;
            }
            case AnchorPresets.BottomRight:
            {
                source.anchorMin = new Vector2(1, 0);
                source.anchorMax = new Vector2(1, 0);
                break;
            }

            case AnchorPresets.HorStretchTop:
            {
                source.anchorMin = new Vector2(0, 1);
                source.anchorMax = new Vector2(1, 1);
                break;
            }
            case AnchorPresets.HorStretchMiddle:
            {
                source.anchorMin = new Vector2(0, 0.5f);
                source.anchorMax = new Vector2(1, 0.5f);
                break;
            }
            case AnchorPresets.HorStretchBottom:
            {
                source.anchorMin = new Vector2(0, 0);
                source.anchorMax = new Vector2(1, 0);
                break;
            }

            case AnchorPresets.VertStretchLeft:
            {
                source.anchorMin = new Vector2(0, 0);
                source.anchorMax = new Vector2(0, 1);
                break;
            }
            case AnchorPresets.VertStretchCenter:
            {
                source.anchorMin = new Vector2(0.5f, 0);
                source.anchorMax = new Vector2(0.5f, 1);
                break;
            }
            case AnchorPresets.VertStretchRight:
            {
                source.anchorMin = new Vector2(1, 0);
                source.anchorMax = new Vector2(1, 1);
                break;
            }

            case AnchorPresets.StretchAll:
            {
                source.anchorMin = new Vector2(0, 0);
                source.anchorMax = new Vector2(1, 1);
                break;
            }
        }
    }

    public static void SetPivot(this RectTransform source, PivotPresets preset) {
        switch (preset)
        {
            case PivotPresets.TopLeft:
            {
                source.pivot = new Vector2(0, 1);
                break;
            }
            case PivotPresets.TopCenter:
            {
                source.pivot = new Vector2(0.5f, 1);
                break;
            }
            case PivotPresets.TopRight:
            {
                source.pivot = new Vector2(1, 1);
                break;
            }

            case PivotPresets.MiddleLeft:
            {
                source.pivot = new Vector2(0, 0.5f);
                break;
            }
            case PivotPresets.MiddleCenter:
            {
                source.pivot = new Vector2(0.5f, 0.5f);
                break;
            }
            case PivotPresets.MiddleRight:
            {
                source.pivot = new Vector2(1, 0.5f);
                break;
            }

            case PivotPresets.BottomLeft:
            {
                source.pivot = new Vector2(0, 0);
                break;
            }
            case PivotPresets.BottomCenter:
            {
                source.pivot = new Vector2(0.5f, 0);
                break;
            }
            case PivotPresets.BottomRight:
            {
                source.pivot = new Vector2(1, 0);
                break;
            }
        }
    }
}