using UnityEngine;

public class TheCube : MonoBehaviour {

    public Color color;
    public float speed = 50f;

    const string EMISSION = "_EmissionColor";

    public Color CurrentColor {
        get { return GetComponent<Renderer>().material.GetColor(EMISSION); }
        set { GetComponent<Renderer>().material.SetColor(EMISSION, value); }
    }

    void Start () {
        CurrentColor = color;
    }
	void Update () {
        transform.Rotate(new Vector3(0.5f, 0.5f, 0.5f), speed * Time.deltaTime);
    }
}
