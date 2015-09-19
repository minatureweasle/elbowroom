using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Fade : MonoBehaviour {

    public static Fade instance;

	public bool fadeInAtStart = true;
	public float fadeDelay = 0.5f;
	public float fadeDuration = 0.4f;

	public Image image;

    //store a static instance for easy access of this class's public functions from other classes
    void Awake(){
        instance = this;
    }

    //fade the scene in (by fading the image out) when the scene starts
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

    //fade the scene out by making the black image opaque (alpha of 1), obscuring the view of the scene
	public void fadeOut(){
		image.CrossFadeAlpha(1, fadeDuration, true);
	}
}
