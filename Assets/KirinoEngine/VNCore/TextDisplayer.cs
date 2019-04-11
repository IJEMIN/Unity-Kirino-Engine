using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace KirinoEngine {
    public class TextDisplayer : MonoBehaviour {
        public GameObject dialougeWindowHolder;

        private string m_currentTypingDialgoue;

        public Text nameDisplayer;
        public Text dialogueDisplayer;

        [Range(0.0f, 1.0f)] public float timeBetUpdateLetters;

        public bool isTyping { get; private set; }

        private void Awake() {
            HideDialogueHolder();
        }

        public void ShowDialogueHolder() {
            dialougeWindowHolder.SetActive(true);
        }

        public void HideDialogueHolder() {
            dialougeWindowHolder.SetActive(false);
        }


        //Skip and Complete Current Dialogues
        public void SkipTypingLetter() {
            StopCoroutine("TypeText");
            StopCoroutine("TypeAndAddText");

            isTyping = false;

            dialogueDisplayer.text = m_currentTypingDialgoue;
        }

        public void SetSay(string dialogue) {
            ShowDialogueHolder();
            m_currentTypingDialgoue = dialogue;

            if (timeBetUpdateLetters <= 0f)
                dialogueDisplayer.text = dialogue;
            else
                StartCoroutine("TypeText", m_currentTypingDialgoue);
        }

        public void SetNameColor(Color nameColor) {
            nameDisplayer.color = nameColor;
        }

        public virtual void SetSay(string speakerName, string dialogue) {
            ShowDialogueHolder();
            nameDisplayer.text = speakerName;
            m_currentTypingDialgoue = dialogue;

            if (timeBetUpdateLetters <= 0f)
                dialogueDisplayer.text = dialogue;
            else
                StartCoroutine("TypeText", m_currentTypingDialgoue);
        }


        //Upddate Text from buffer
        public IEnumerator TypeText(string texts) {
            isTyping = true;

            ShowDialogueHolder();

            dialogueDisplayer.text = string.Empty;

            foreach (var letter in texts)
            {
                dialogueDisplayer.text += letter;
                yield return new WaitForSeconds(timeBetUpdateLetters);
            }

            isTyping = false;
        }

        // Add Text, not replace 
        public IEnumerator TypeAndAddText(string texts) {
            isTyping = true;

            ShowDialogueHolder();

            foreach (var letter in texts)
            {
                dialogueDisplayer.text += letter;
                yield return new WaitForSeconds(timeBetUpdateLetters);
            }

            isTyping = false;
        }
    }
}