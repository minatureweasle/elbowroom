using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

	//int playerId;
	//string localPlayerName;
	//public GameObject localPlayer;
	bool playing = false;
	public Text timeText;

	float time;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (playing) {
			time += Time.deltaTime;

			timeText.text = time + "";
		}
	
	}

	public void startTime(){
		playing = true;
	}

	public void saveTime(){

		ScoreManager.instance.setLastScore (time);


		playing = false;
	}
}
