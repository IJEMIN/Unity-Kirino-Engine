using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DisplayableDisplayer : MonoBehaviour {

	public CanvasGroup displayablesHolder;

	// key: displayable unique (it automatically defined as filename)
	// tag: tag define shared base between many displayable.

	// Replace exist image (mostly for face replacing) require both key and tag

	// exampale:
	// tag: kirino, key: kirino_angry  and tag: kirino, key: kirino_happy
	// show kirino_happy will replace kirino_angry (if it alreay exists);
	
	// Replaceing only happening between images which have same tag with diffrent key

	// dic key : tag of displayable
	public Dictionary<string,Image> images = new Dictionary<string,Image>();

	public void Show(Displayable displayable)
	{
		// if tag is not defined, use key as tag
		if(string.IsNullOrEmpty(displayable.tag))
		{
			AddImage(displayable.key,displayable.sprite);
			return;
		}

		if(images.ContainsKey(displayable.tag))
		{
			ReplaceImage(displayable.tag,displayable.sprite);
		}
		else
		{
			AddImage(displayable.tag,displayable.sprite);
		}
	}

	private void AddImage(string tag, Sprite sprite)
	{
		var image = new GameObject(tag).AddComponent<Image>();
		image.sprite = sprite;
		image.transform.SetParent(displayablesHolder.transform);

		image.transform.localScale = Vector3.one;
		image.SetNativeSize();
		image.rectTransform.anchoredPosition = Vector2.zero;

        images.Add(tag, image);
	}

	private void ReplaceImage(string tag, Sprite sprite)
	{
		images[tag].sprite = sprite;
	}

	private IEnumerator Show()
	{
		yield return null;
	}
	public void Hide(string tag)
	{
		var target = images[tag].gameObject;
		images.Remove(tag);
		Destroy(target);
	}

	public void HideAll()
	{
		foreach(var imageTag in images.Keys)
		{
			Destroy(images[imageTag].gameObject);
		}

		images.Clear();
	}
}