using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using MyExtensions;

public class DisplayableDisplayer : MonoBehaviour {

    public float dissolveTime = 0.5f;

    // Enqueue `true` when a drawing coroutine starts,
    // and dequeue when one ends.
    private Queue<bool> currentDrawingRoutine = new Queue<bool>();


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

        StartCoroutine("DissolveIn", image);
	}

	private void ReplaceImage(string tag, Sprite sprite)
	{
        // prevImage is not inserted into images list.
        var prevImage = Instantiate(images[tag], images[tag].transform.parent);
        prevImage.name += "_prev";

		images[tag].sprite = sprite;

        StartCoroutine("DissolveOutAndDestroy", prevImage);
        StartCoroutine("DissolveIn", images[tag]);
	}


	public void Hide(string tag)
	{
        var targetImage = images[tag].GetComponent<Image>();
		images.Remove(tag);

        StartCoroutine("DissolveOutAndDestroy", targetImage);
	}

	public void HideAll()
	{
		foreach(var imageTag in images.Keys)
		{
            StartCoroutine("DissolveOutAndDestroy", images[imageTag]);
		}

		images.Clear();
	}



    IEnumerator DissolveIn(Image image)
    {
        currentDrawingRoutine.Enqueue(true);

        float alpha = 0.0f;
        image.SetTransparency(alpha);

        float startTime = Time.time;

        while (Time.time <= startTime + dissolveTime)
        {
            alpha += (Time.deltaTime / dissolveTime);
            image.SetTransparency(alpha);

            yield return null;
        }

        image.SetTransparency(1.0f);

        currentDrawingRoutine.Dequeue();
    }

    IEnumerator DissolveOutAndDestroy(Image image)
    {
        currentDrawingRoutine.Enqueue(true);

        float alpha = 1.0f;
        image.SetTransparency(alpha);

        float startTime = Time.time;

        while (Time.time <= startTime + dissolveTime)
        {
            alpha -= (Time.deltaTime / dissolveTime);
            image.SetTransparency(alpha);

            yield return null;
        }

        currentDrawingRoutine.Dequeue();

        Destroy(image.gameObject);
    }
}