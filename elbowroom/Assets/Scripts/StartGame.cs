using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StartGame : MonoBehaviour {

	public GameObject[] players;

	public Text countdownText;

	//blocks the player until the race begins
	public GameObject invisibleWall;

	//start the timers for each player and disable the invisible wall
	public void NewRace(){
		for (int i = 0; i < players.Length; i++) {
			TimeManager tM = players[i].GetComponent<TimeManager>();
			tM.startTimer();
			invisibleWall.SetActive(false);
		}
	}

	//start the race countdown. The race begins when it reaches 0
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
		NewRace ();
		yield return new WaitForSeconds (0.5f);
		countdownText.text = "";
	}
}
