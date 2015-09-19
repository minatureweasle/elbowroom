using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public static TimeManager instance;

	public Text timeText;
    public Text timeText2;

	float time = 0;

	bool playing = false;

    //store a static instance for easy access of this class's public functions from other classes
    void Awake(){
        instance = this;
    }

    //increase the time since the race began by the time between frames
    //and write the times to the canvas's Text elements
	void Update () {
		if (playing) {
			time += Time.deltaTime;
			timeText.text = time + "";
            timeText2.text = time + "";
		}
	}

    //start increasing the time every frame
	public void startTimer(){
        time = 0;
		playing = true;
	}

    //stop the timer, store the new time for this race, and store the winner of this race
	public void saveTime(string winner){
        if (playing) {
		    playing = false;
		    ScoreManager.instance.setLastScore (Application.loadedLevelName, time);
            ScoreManager.instance.WriteWinnerName(Application.loadedLevelName, winner);
        }
	}

    public bool gameIsUnderway()
    {
        return playing;
    }
}
