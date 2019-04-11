namespace KirinoEngine {
    public class VNSetAnchor : VNCommand {
        public AnchorPresets anchor;

        public string displayableName;

        public VNSetAnchor(string _displayableName, AnchorPresets _anchor) {
            displayableName = _displayableName;
            anchor = _anchor;
        }

        public override void Invoke() {
            VNController.displayableDisplayer.UpdateAnchor(displayableName, anchor);
        }
    }
}