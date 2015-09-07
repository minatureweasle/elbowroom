using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Fade : MonoBehaviour {

	public bool fadeIn;
	public float fadeTime;
	//public float delay;

	public Image vignette;

	void Start () {
		//renderer.material.color = new Color(0,0,0,1);
		if (fadeIn){

			StartCoroutine(fadeInLogoCoroutine());
		}
	}
	
	void Update () {
//		fadeTime = fadeTime - Time.deltaTime;
//		renderer.material.color -= new Color(0,0,0, 2f * Time.deltaTime);
//
	//	if (fadeTime < 0)
	//		Destroy (gameObject);

	}	

	IEnumerator fadeInLogoCoroutine(){

		vignette.color = Color.white;

		yield return new WaitForSeconds(0.5f);

		vignette.CrossFadeAlpha(0, fadeTime, true);

	}
}
