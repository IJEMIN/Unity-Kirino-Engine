namespace KirinoEngine {
    public class VNSetPivot : VNCommand {
        public string displayableName;
        public PivotPresets pivot;

        public VNSetPivot(string _displayableName, PivotPresets _pivot) {
            displayableName = _displayableName;
            pivot = _pivot;
        }

        public override void Invoke() {
            VNController.displayableDisplayer.UpdatePivot(displayableName, pivot);
        }
    }
}