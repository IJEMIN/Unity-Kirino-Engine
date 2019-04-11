using UnityEngine;

namespace KirinoEngine {
    public class VNSetPosition : VNCommand {
        public string displayableName;
        public Vector2 newAnchoredPosition;

        public VNSetPosition(string _displayableName, Vector2 _newAnchordPosition) {
            displayableName = _displayableName;
            newAnchoredPosition = _newAnchordPosition;
        }

        public override void Invoke() {
            VNController.displayableDisplayer.UpdatePosition(displayableName, newAnchoredPosition);
        }
    }
}