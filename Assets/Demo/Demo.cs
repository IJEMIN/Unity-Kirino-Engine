using System.Collections;
using System.Collections.Generic;
using KirinoEngine;
using UnityEngine;

public class Demo : MonoBehaviour
{
    private Stack<VNCommand> commandStack = new Stack<VNCommand>();

    public Sprite[] demoBackgroundSprites;
    public Sprite kirinoSprite;
    // Start is called before the first frame update
    void Start()
    {
        var kiririnDisplayable = new Displayable();
        kiririnDisplayable.canvasSize = new Vector2(603,1024);
        kiririnDisplayable.tag = "Idle";
        kiririnDisplayable.name = "Kirino";
        
        kiririnDisplayable.spriteMappers.Add(new SpriteMapper {pos = Vector2.zero, size = new Vector2(603,1024), 
        sprite =  kirinoSprite});
        
        
        commandStack.Push(new VNBackground(demoBackgroundSprites[0]));

        commandStack.Push(new VNSay("Kriring","Hi!"));
        commandStack.Push(new VNShow(kiririnDisplayable));
        commandStack.Push(new VNSay("Kriring","THis is Demo of Kirino Engine!"));


        commandStack.Push(new VNSay("Kriring","Kirino Engine provide many Core Manager"));
        commandStack.Push(new VNSay("Kriring","Which you can control by stacking VNCommand or Manually calling it's own method within Cores"));
        
        
        commandStack.Push(new VNBackground(demoBackgroundSprites[1]));
        commandStack.Push(new VNSay("Kriring","Please Check demo script"));
        commandStack.Push(new VNSay("Kriring","Kirino Engine only provides functions -not script functions to call kirino function"));
        commandStack.Push(new VNSay("Kriring","that means you have to make your own parser to implement 'delayed call'"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && commandStack.Count > 0)
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


            commandStack.Pop().Invoke();
        }
    }
}
