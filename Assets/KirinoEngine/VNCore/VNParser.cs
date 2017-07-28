using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGPrompter;
using UnityEngine.UI;

public class VNParser : MonoBehaviour {

	public string scriptPath;

	public VNEpisode episode;

	const string syntax_background = "scene";
	const string syntax_show = "show";

	Script m_rpyScript;
	void Start () {
		m_rpyScript = Script.FromSource(scriptPath);

		m_rpyScript.Prime();
		m_rpyScript.Validate();

		StartCoroutine(m_rpyScript.GetCurrentEnumerator(OnLine,SelectChoice,OnChoiceSelected,OnReturn));
	}


	// 리턴을 만났을때
    IEnumerator OnReturn() {

        // Show a last message when the script ends
		Debug.Log("end");
        yield return new WaitForSecondsRealtime(2f);
    }

	// 선택지가 선택됬을때 무엇을 할까
	// 입력으론 선택된 것이 온다
    IEnumerator OnChoiceSelected(Script.Menu menu, Script.Choice choice) {

        // Show the choice text after the choice has been selected
		Debug.Log(choice.ToString());
        yield return new WaitForSecondsRealtime(2f);
    }

	//선택을 어떻게 입력할까, 입력을 구현하는 곳
	// 정확히는 여러 인덱스 중 하나를 골라서 내보내는 것을 구현
    IEnumerator<int?> SelectChoice(Script.Menu menu) {

        // Show the menu and return the choice index once selected
        yield return menu.TrueChoices[0].Index;
    }


	// 각각의 대사 라인 처리
	IEnumerator OnLine(Script.DialogueLine line)
	{
		Debug.Log(line.Tag);
		Debug.Log(line.Text);


		switch(line.Tag)
		{
			case syntax_background:
				episode.AddCommand(new VNBackground(VNDataContainer.Instance.GetDisplayable(line.Text)));
				yield break;
			break;
			
			case syntax_show:
				episode.AddCommand(new VNShow(VNDataContainer.Instance.GetDisplayable(line.Text)));
				yield break;
			break;
		}


		episode.AddCommand(new VNSay(line.Tag,line.Text));

		yield return null;
	}


}
