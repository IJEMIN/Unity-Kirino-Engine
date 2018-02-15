using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;


public class MenuDisplayer : MonoBehaviour
{
    private SimpleObjectPool m_buttonPool;

    public GameObject selectionButtonPrefab;

    public List<SelectionButton> selectionButtons;

    public MaskableGraphic rayBlocker;


    private void Start()
    {
        rayBlocker.enabled = false;
        m_buttonPool = new SimpleObjectPool();
        m_buttonPool.prefab = selectionButtonPrefab;
    }

    public void RemoveAllSelections() {

        rayBlocker.enabled = false;

        while (0 < selectionButtons.Count)
        {
            var buttonRemoved = selectionButtons[0];
            selectionButtons.RemoveAt(0);

            m_buttonPool.ReturnObject(buttonRemoved.gameObject);
        }
    }

	public void AddNewSelection(string choiceName,int index) 
    {

        rayBlocker.enabled = true;

        SelectionButton newSelectionButton = m_buttonPool.GetObject().GetComponent<SelectionButton>();
        newSelectionButton.Init(choiceName,index);

        selectionButtons.Add(newSelectionButton);
        newSelectionButton.transform.SetParent(this.transform);
        newSelectionButton.transform.SetAsLastSibling();
        newSelectionButton.GetComponent<RectTransform>().localScale = Vector3.one;
    }


	public SelectionButton selectedButton
    {
        get
        {
            for (int i = 0; i < selectionButtons.Count; i++)
            {
                if(selectionButtons[i].isSelected)
                {
                    return selectionButtons[i];
                }
            }
            return null;
        }
    }
}
