
namespace KirinoEngine
{
    using VNCore;

    public class VNHide : VNCommand
    {

        public string tag;

        public VNHide(string tag_)
        {
            tag = tag_;
        }

        public override void Invoke()
        {
            VNLocator.displayableDisplayer.Hide(tag);
            VNLocator.currentEpisode.InvokeNextCommand();
        }
    }
}
