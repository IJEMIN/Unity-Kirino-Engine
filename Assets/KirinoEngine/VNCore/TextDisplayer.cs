using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TextDisplayer : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float timeBetUpdateLetters;


    public Text nameDisplayer;
    public Text sayDisplayer;

    public GameObject dialougeWindowHolder;


    public bool isTyping
    {
        get; private set;
    }

    private string m_currentTypingDialgoue;

    public void ShowDialogueHolder()
    {
        dialougeWindowHolder.SetActive(true);
    }

    public void HideDialogueHolder()
    {
        dialougeWindowHolder.SetActive(false);
    }



    //Skip and Complete Current Dialogues
    public void SkipTypingLetter()
    {
        StopCoroutine("TypeText");
        StopCoroutine("TypeAndAddText");

        isTyping = false;

        sayDisplayer.text = m_currentTypingDialgoue;
    }


    public void SetSay(string dialogue)
    {
        m_currentTypingDialgoue = dialogue;

        if (timeBetUpdateLetters <= 0f)
        {
            sayDisplayer.text = dialogue;
        }
        else
        {
            StartCoroutine("TypeText", m_currentTypingDialgoue);
        }
    }

    public void SetSay(string speakerName, string dialogue)
    {
        nameDisplayer.text = speakerName;
        m_currentTypingDialgoue = dialogue;

        if (timeBetUpdateLetters <= 0f)
        {
            sayDisplayer.text = dialogue;
        }
        else
        {
            StartCoroutine("TypeText", m_currentTypingDialgoue);
        }
    }


    //Upddate Text from buffer
    public IEnumerator TypeText(string texts)
    {
        isTyping = true;

        ShowDialogueHolder();

        sayDisplayer.text = string.Empty;

        foreach (char letter in texts.ToCharArray())
        {
            sayDisplayer.text += letter;
            yield return new WaitForSeconds(timeBetUpdateLetters);
        }

        isTyping = false;
    }

    // Add Text, not replace 
    public IEnumerator TypeAndAddText(string texts)
    {
        isTyping = true;

        ShowDialogueHolder();

        foreach (char letter in texts.ToCharArray())
        {

            sayDisplayer.text += letter;
            yield return new WaitForSeconds(timeBetUpdateLetters);

        }

        isTyping = false;
    }


}
