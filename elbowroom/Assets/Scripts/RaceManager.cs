using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RaceManager : MonoBehaviour {

    public static RaceManager instance;

	public Text countdownText;

	//blocks the player until the race begins
	public GameObject invisibleWall;

    //store a static instance for easy access of this class's public functions from other classes
    void Awake()
    {
        instance = this;
    }

	//start the timers for each player and disable the invisible wall
	void NewRace()
    {
        TimeManager.instance.startTimer();
        invisibleWall.SetActive(false);
	}

    //activate the timer's function to handle the end of a race with the winner's name
    public void EndRace(string winner)
    {
        TimeManager.instance.saveTime(winner);
    }

	//start the race countdown. The race begins when it reaches 0
	public void StartCountdown(){
		StartCoroutine (Countdown());
	}

    //display numbers counting down every second from 3, and start the race at 0 seconds
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
