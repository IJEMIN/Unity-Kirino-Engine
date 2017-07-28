using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_MakeDisplayer : MonoBehaviour {

    [Header("References")]
    public DisplayableDisplayer displayableDisplayer;

    [Header("Options")]
    public Displayable displayable;

    public void ShowDisplayable() 
    {
        displayableDisplayer.Show(displayable);
    }

    public void HideDisplayable()
    {
        displayableDisplayer.Hide(displayable.tag);
    }
}
