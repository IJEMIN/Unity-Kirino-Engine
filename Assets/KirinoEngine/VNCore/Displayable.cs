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

    public Sprite sprite;
}
