using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KirinoEngine;
using UnityEngine;

public class SwitchDemo : MonoBehaviour
{
    private List<VNCommand> commandStack = new List<VNCommand>();
    private IEnumerator<VNCommand> interator;

    public Sprite[] demoBackgroundSprites;
    public Sprite kirinoSprite;
    // Start is called before the first frame update
    void Start()
    {
        var kiririnDisplayable = new Displayable();
        kiririnDisplayable.canvasSize = new Vector2(603, 1024);
        kiririnDisplayable.tag = "Idle";
        kiririnDisplayable.name = "Kirino";

        kiririnDisplayable.spriteMappers.Add(new SpriteMapper
        {
            pos = Vector2.zero,
            size = new Vector2(603, 1024),
            sprite = kirinoSprite
        });
        commandStack.Add(new VNSwitch("intro"));
        commandStack.Add(new VNSay("Kriring", "SwitchDemoDone"));
        commandStack.Add(new VNSay("Kriring", "That's All. :)"));
        commandStack.Add(new VNLoadScene("Demo"));
        interator = commandStack.GetEnumerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (VNController.backgroundDisplayer.IsDrawing || VNController.displayableDisplayer.isDrawing)
            {
                return;
            }


            if (VNController.textDisplayer.isTyping)
            {
                VNController.textDisplayer.SkipTypingLetter();
                return;
            }

            if (interator.MoveNext())
            {
                interator.Current.Invoke();
            }
            else
            {
                interator.Reset();

            }
        }
    }
}
