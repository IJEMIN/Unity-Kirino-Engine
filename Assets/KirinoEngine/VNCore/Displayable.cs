using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "displayable", menuName = "Empty Displayable", order = 1)]
public class Displayable : ScriptableObject
{

    public string key
	{
		get
		{
			//use scriptable object filename as key
			return name;
		}
	}

	// replace prviouse displayable as showing new displayable with same tag
	public string tag;

    public Sprite sprite;
}
