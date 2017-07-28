using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DisplayableDisplayer : MonoBehaviour {

	public CanvasGroup displayablesHolder;

	// key: tag of displayable. when tag is empty, Displayable.key is key of dictionary.
	// for replacing if two different displayables has same tag. mostly for face replacing.
	// example: tag: kirino, key: kirino_angry  and tag: kirino, key: kirino_happy
	public Dictionary<string,ScriptableObject> currentDisplayables = new Dictionary<string,ScriptableObject>();
	public Dictionary<string,Image> images = new Dictionary<string,Image>();

	public void Show(Displayable displayable)
	{
		if(string.IsNullOrEmpty(displayable.tag))
		{
			currentDisplayables.Add(displayable.key,displayable);
			AddImage(displayable.key,displayable.sprite);
		}
		else
		{
			// there is already image which have same tag
			if(currentDisplayables.ContainsKey(displayable.tag))
			{
				currentDisplayables[displayable.tag] = displayable;
                ReplaceImage(displayable.tag,displayable.sprite);
			}
			else
			{
                currentDisplayables.Add(displayable.tag, displayable);
				AddImage(displayable.tag,displayable.sprite);
			}
		}
	}

	private void AddImage(string key, Sprite sprite)
	{
		var image = new GameObject(key).AddComponent<Image>();
		image.sprite = sprite;
		image.transform.SetParent(displayablesHolder.transform);

		image.transform.localScale = Vector3.one;
		image.SetNativeSize();
		image.rectTransform.anchoredPosition = Vector2.zero;

        images.Add(key, image);
	}

	private void ReplaceImage(string key, Sprite sprite)
	{
		images[key].sprite = sprite;
	}

	private IEnumerator Show()
	{
		yield return null;
	}
	public void Hide(string key)
	{
		Destroy(images[key].gameObject);

		currentDisplayables.Remove(key);
		images.Remove(key);
	}

	public void HideAll()
	{
		foreach(var currentDisplayable in currentDisplayables)
		{
			//memo: it's a KeyValuePair's key, not a key of Displayable.
			Hide(currentDisplayable.Key);
		}
	}
}