using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundDisplayer : MonoBehaviour {

	public float dissolveSpeed = 1.0f;
	Image m_backgroundDisplayer;

	public bool isChanging{
		get;
		private set;
	}

	void Awake () {
		m_backgroundDisplayer = GetComponent<Image>();	
	}

	public void ChangeBackground(Sprite newBackground)
	{
		VNLocator.displayableDisplayer.HideAll();
		VNLocator.textDisplayer.HideDialogueHolder();

		StopCoroutine("SwitchBackgroundSprite");
		StartCoroutine("SwitchBackgroundSprite",newBackground);
	}

	private IEnumerator SwitchBackgroundSprite(Sprite newBackground)
	{
		isChanging = true;

		var lastTimeCheck = Time.time;
		var estimatedTime = 1.0f/dissolveSpeed;

		while(lastTimeCheck + estimatedTime >= Time.time)
		{
			var color = m_backgroundDisplayer.color;
			color.a -= Time.deltaTime * dissolveSpeed;
			m_backgroundDisplayer.color = color;
			yield return null;
		}

		m_backgroundDisplayer.sprite = newBackground;

		lastTimeCheck = Time.time;

		while(lastTimeCheck + estimatedTime >= Time.time)
		{
			var color = m_backgroundDisplayer.color;
			color.a += Time.deltaTime * dissolveSpeed;
			m_backgroundDisplayer.color = color;
			yield return null;
		}
		
		m_backgroundDisplayer.color = Color.white;

		isChanging = false;
	}
}
