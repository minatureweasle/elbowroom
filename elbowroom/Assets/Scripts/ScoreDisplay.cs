using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	GameObject scoreManager;

	public Text lastScoreText;
	public Text bestScoreText;
	public Text welcomeText;

	public GameObject myPortal;

	void Start () {

		string doorLeadsTo = myPortal.GetComponent<SwitchScene> ().room;

		scoreManager = GameObject.Find ("_ScoreManager");
		lastScoreText.text = scoreManager.GetComponent<ScoreManager> ().getLastScoreAsString (doorLeadsTo);
		bestScoreText.text = scoreManager.GetComponent<ScoreManager> ().getBestScoreAsString (doorLeadsTo);
		welcomeText.text = doorLeadsTo + "\n" + "winner: " + scoreManager.GetComponent<ScoreManager>().getWinner (doorLeadsTo);
	
	}
}
