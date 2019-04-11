using UnityEngine;

namespace KirinoEngine {
    public class VNSetSize : VNCommand {
        public string displayableName;
        public Vector2 newDeltaSize;


        public VNSetSize(string _displayableName, Vector2 _newDeltaSize) {
            displayableName = _displayableName;
            newDeltaSize = _newDeltaSize;
        }

        public override void Invoke() {
            VNController.displayableDisplayer.UpdateSize(displayableName, newDeltaSize);
        }
    }
}