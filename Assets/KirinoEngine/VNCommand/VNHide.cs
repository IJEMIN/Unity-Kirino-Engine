namespace KirinoEngine {
    public class VNHide : VNCommand {
        public string displayableTag;

        public VNHide(string _displayableTag) {
            displayableTag = _displayableTag;
        }

        public override void Invoke() {
            VNController.displayableDisplayer.Hide(displayableTag);
        }
    }
}