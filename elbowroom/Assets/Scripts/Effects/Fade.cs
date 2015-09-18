using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Fade : MonoBehaviour {

    public static Fade instance;

	public bool fadeInAtStart = true;
	public float fadeDelay = 0.5f;
	public float fadeDuration = 0.4f;

	public Image image;

    void Awake(){
        instance = this;
    }

	void Start () {
		if (fadeInAtStart){
			StartCoroutine(fadeInLogoCoroutine());
		}
	}

	//make the image opaque, wait a moment, then fade it away
	IEnumerator fadeInLogoCoroutine(){
		image.color = Color.white;
		yield return new WaitForSeconds (fadeDelay);
		image.CrossFadeAlpha(0, fadeDuration, true);
	}

	public void fadeOut(){
		image.CrossFadeAlpha(1, fadeDuration, true);
	}
}
