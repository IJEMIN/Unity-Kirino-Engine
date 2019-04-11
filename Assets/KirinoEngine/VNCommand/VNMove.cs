using UnityEngine;

namespace KirinoEngine {
    public class VNMove : VNCommand {
        public string displayableTag;
        public float msec;
        public Vector2 targetPosition;
        public DisplayableDisplayer.TweenType tweenType;

        public VNMove(string _displayableTag, Vector2 _targetPosition, DisplayableDisplayer.TweenType _tweenType,
            float _msec) {
            displayableTag = _displayableTag;
            targetPosition = _targetPosition;
            tweenType = _tweenType;
            msec = _msec;
        }

        public override void Invoke() {
            VNController.displayableDisplayer.MoveDisplayable(displayableTag, targetPosition, msec * 0.001f, tweenType);
        }
    }
}