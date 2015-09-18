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

		string doorLeadsTo = myPortal.GetComponent<DoorPortal> ().room;

		lastScoreText.text = ScoreManager.instance.getLastScoreAsString (doorLeadsTo);
		bestScoreText.text = ScoreManager.instance.getBestScoreAsString (doorLeadsTo);
		welcomeText.text = doorLeadsTo + "\n" + "winner: " + ScoreManager.instance.getWinner (doorLeadsTo);
	
	}
}
