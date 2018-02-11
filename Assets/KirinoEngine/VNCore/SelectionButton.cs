﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionButton : MonoBehaviour {

    public int index;
    public bool isSelected;

    public Text m_text;

    public void Init(string selectionText, int index)
    {
        m_text.text = selectionText;
        this.index = index;

        isSelected = false;
    }

    public void OnSelect()
    {
        isSelected = true;
    }
}
