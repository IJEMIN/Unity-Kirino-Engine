namespace KirinoEngine {
    public class VNShow : VNCommand {
        public Displayable displayable;

        public VNShow(Displayable displayable_) {
            displayable = displayable_;
        }

        public override void Invoke() {
            VNController.displayableDisplayer.Show(displayable);
        }
    }
}