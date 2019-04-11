using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KirinoEngine {
    public class MenuDisplayer : MonoBehaviour {
        private SimpleObjectPool m_buttonPool;

        public MaskableGraphic rayBlocker;

        public GameObject selectionButtonPrefab;

        public List<SelectionButton> selectionButtons;

        public bool isSelecting
        {
            get
            {
                if (selectionButtons.Count > 0) return true;
                return false;
            }
        }


        public SelectionButton selectedButton
        {
            get
            {
                for (var i = 0; i < selectionButtons.Count; i++)
                    if (selectionButtons[i].isSelected)
                        return selectionButtons[i];
                return null;
            }
        }


        private void Start() {
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

        public void AddNewSelection(string choiceName, int index) {
            VNController.textDisplayer.HideDialogueHolder();
            rayBlocker.enabled = true;

            var newSelectionButton = m_buttonPool.GetObject().GetComponent<SelectionButton>();
            newSelectionButton.Init(choiceName, index);

            selectionButtons.Add(newSelectionButton);
            newSelectionButton.transform.SetParent(transform);
            newSelectionButton.transform.SetAsLastSibling();
            newSelectionButton.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}