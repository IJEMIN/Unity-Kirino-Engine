using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DisplayableDisplayer : MonoBehaviour {

	public CanvasGroup displayablesHolder;

	public List<ScriptableObject> currentDisplayables;

	public void HideImage()
	{
		StartCoroutine("Hide");
	}

	public void DrawImage(Sprite sprite)
	{

	}

	private IEnumerator Show()
	{
		yield return null;
	}

	private IEnumerator Hide()
	{
		yield return null;
	}



}