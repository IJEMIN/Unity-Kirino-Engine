using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using KirinoEngine;
using UnityEngine;

public class Demo : MonoBehaviour
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

        commandStack.Add(new VNSay("Halooooooo"));

        commandStack.Add(new VNBackground(demoBackgroundSprites[0]));

        commandStack.Add(new VNSay("Kriring", "Hi!"));
        commandStack.Add(new VNShow(kiririnDisplayable));
        commandStack.Add(new VNSay("Kriring", "THis is Demo of Kirino Engine!"));
        commandStack.Add(new VNSwitch("intro",true));

        commandStack.Add(new VNSwitch("SwitchOffTest",true));
        commandStack.Add(new VNSay("Kriring", "Kirino Engine provide many Core Manager"));
        commandStack.Add(new VNSay("Kriring", "Which you can control by stacking VNCommand or Manually calling it's own method within Cores"));
        commandStack.Add(new VNSwitch("SwitchOffTest", false));
        commandStack.Add(new VNSwitchGoto("SwitchOffTest", "GOTOAnoterScene")); // when Switch Stats off

        commandStack.Add(new VNBackground(demoBackgroundSprites[1]));
        commandStack.Add(new VNSay("Kriring", "Please Check demo script"));
        commandStack.Add(new VNSay("Kriring", "Kirino Engine only provides functions -not script functions to call kirino function"));
        commandStack.Add(new VNSay("Kriring", "that means you have to make your own parser to implement 'delayed call'"));
        commandStack.Add(new VNSwitchGoto("intro", "GOTOAnoterScene")); // When <intro> switch on Goto <GOTOAnoterScene>
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
