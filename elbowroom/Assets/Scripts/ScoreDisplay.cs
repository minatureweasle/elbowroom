using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	public Text lastScoreText;
	public Text bestScoreText;
	public Text welcomeText;

	public GameObject myPortal;

    //Fetch this room's scores and winner and show them on the canvas as text
	void Start () {

		string doorLeadsTo = myPortal.GetComponent<DoorPortal> ().room;

		lastScoreText.text = ScoreManager.instance.getLastScoreAsString (doorLeadsTo);
		bestScoreText.text = ScoreManager.instance.getBestScoreAsString (doorLeadsTo);
		welcomeText.text = doorLeadsTo + "\n" + "winner: " + ScoreManager.instance.getWinner (doorLeadsTo);
	
	}
}
