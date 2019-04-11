namespace KirinoEngine {
    public class VNScale : VNCommand {
        public string displayableTag;
        public float msec;
        public float targetScale;
        public DisplayableDisplayer.TweenType tweenType;

        public VNScale(string _displayableTag, float _targetScale, DisplayableDisplayer.TweenType _tweenType,
            float _msec) {
            displayableTag = _displayableTag;
            targetScale = _targetScale;
            tweenType = _tweenType;
            msec = _msec;
        }

        public override void Invoke() {
            VNController.displayableDisplayer.ScaleDisplayable(displayableTag, targetScale, msec * 0.001f, tweenType);
        }
    }
}