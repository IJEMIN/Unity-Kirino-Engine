
namespace KirinoEngine
{
    public class VNHide : VNCommand
    {

        public string tag;

        public VNHide(string tag_)
        {
            tag = tag_;
        }

        public override void Invoke()
        {
            VNController.displayableDisplayer.Hide(tag);
        }
    }
}
