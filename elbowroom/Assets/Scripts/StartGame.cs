using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StartGame : MonoBehaviour {

	public GameObject[] players;

	public Text countdownText;

	//blocks the player until the race begins
	public GameObject invisibleWall;

	public void NewRace(){

		for (int i = 0; i < players.Length; i++) {
			TimeManager tM = players[i].GetComponent<TimeManager>();
			tM.startTime();

			invisibleWall.SetActive(false);
		}

	}

	public void StartCountdown(){
		StartCoroutine (Countdown());
	}

	IEnumerator Countdown(){

		countdownText.text = "3";

		yield return new WaitForSeconds (1);

		countdownText.text = "2";
		
		yield return new WaitForSeconds (1);

		countdownText.text = "1";
		
		yield return new WaitForSeconds (1);

		countdownText.text = "RACE!";

		yield return new WaitForSeconds (0.5f);

		countdownText.text = "";

		NewRace ();

	}
}
