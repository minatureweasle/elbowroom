using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class RaceManager : MonoBehaviour {

    public static RaceManager instance;

	public Text countdownText;

	//blocks the player until the race begins
	public GameObject invisibleWall;

    void Awake()
    {
        instance = this;
    }

	//start the timers for each player and disable the invisible wall
	void NewRace()
    {
        //PlayerGroup.instance.startTimers();
        TimeManager.instance.startTimer();
        invisibleWall.SetActive(false);
	}

    public void EndRace(string winner)
    {
        //PlayerGroup.instance.stopTimers();
        TimeManager.instance.saveTime(winner);
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
