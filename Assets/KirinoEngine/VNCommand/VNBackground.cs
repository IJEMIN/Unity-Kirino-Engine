using UnityEngine;
using System.Collections;

public class VNBackground : VNCommand
{

    public Displayable displayable;

	public VNBackground(Displayable displayable_)
	{
		displayable = displayable_;
	}
    public override void Invoke()
    {
        VNLocator.backgroundDisplayable.ChangeBackground(displayable.sprite);
		//todo: invoke command of current VNEpisode instance
    }
}