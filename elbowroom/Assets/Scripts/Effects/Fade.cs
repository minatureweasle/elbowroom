using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	public bool fadeIn;
	public bool fadeOut;
	public float fadeTime;
	public float delay;

	void Start () {
		renderer.material.color = new Color(0,0,0,1);
	}
	
	void Update () {
		fadeTime = fadeTime - Time.deltaTime;
		renderer.material.color -= new Color(0,0,0, 2f * Time.deltaTime);

		if (fadeTime < 0)
			Destroy (gameObject);
	}	
}
