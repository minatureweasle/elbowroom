using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	public Text timeText;

	float time = 0;

	bool playing = false;

	void Update () {
		if (playing) {
			time += Time.deltaTime;
			timeText.text = time + "";
		}
	}

	public void startTimer(){
		playing = true;
	}

	public void saveTime(string playerName){
		playing = false;
		ScoreManager.instance.setLastScore (Application.loadedLevelName, time);
		ScoreManager.instance.WriteWinnerName(Application.loadedLevelName, playerName);
	}
}
