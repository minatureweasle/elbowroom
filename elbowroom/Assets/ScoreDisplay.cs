using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	GameObject scoreManager;

	public Text lastScoreText;
	public Text bestScoreText;

	// Use this for initialization
	void Start () {

		scoreManager = GameObject.Find ("_ScoreManager");
		lastScoreText.text = scoreManager.GetComponent<ScoreManager> ().getLastScoreAsString ();
		bestScoreText.text = scoreManager.GetComponent<ScoreManager> ().getBestScoreAsString ();
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
