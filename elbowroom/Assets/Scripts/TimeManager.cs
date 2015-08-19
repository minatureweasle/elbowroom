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

	void saveTime(int playerId){




		playing = false;
	}
}
