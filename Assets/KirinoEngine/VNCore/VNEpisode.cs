
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
public class VNEpisode : MonoBehaviour
{

    public void AddCommand(VNCommand command)
    {
        m_commands.Add(command);
    }


    public List<VNCommand> m_commands = new List<VNCommand>();

    public bool isPausing
    {
        get;
        private set;
    }

    private IEnumerator<VNCommand> iterator;
    
    public void PauseForSeconds(float sec)
    {
        isPausing = true;
        Invoke("UnPause", sec);
    }

    private void UnPause()
    {
        isPausing = false;
        InvokeNextCommand();
    }

    public void InvokeNextCommand()
    {

        if (isPausing)
        {
            return;
        }

        if (VNLocator.textDisplayer.isTyping)
        {
            VNLocator.textDisplayer.SkipTypingLetter();
            return;
        }

        if(iterator == null)
        {
            InitiateScript();
        }

        if (iterator.MoveNext())
        {
            iterator.Current.Invoke();
        }
        else
        {
            Debug.LogWarning("End of Story");
        }
    }

    public void InitiateScript()
    {
        iterator = null;
        isPausing = false;

        iterator = m_commands.GetEnumerator();
    }

}
