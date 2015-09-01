using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	GameObject scoreManager;

	public Text lastScoreText;
	public Text bestScoreText;

	public GameObject myPortal;

	// Use this for initialization
	void Start () {

		string doorLeadsTo = myPortal.GetComponent<SwitchScene> ().room;

		scoreManager = GameObject.Find ("_ScoreManager");
		lastScoreText.text = scoreManager.GetComponent<ScoreManager> ().getLastScoreAsString (doorLeadsTo);
		bestScoreText.text = scoreManager.GetComponent<ScoreManager> ().getBestScoreAsString (doorLeadsTo);
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
