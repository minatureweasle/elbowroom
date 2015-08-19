using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameObject[] players;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewRace(){

		for (int i = 0; i < players.Length; i++) {
			TimeManager tM = players[i].GetComponent<TimeManager>();
			tM.startTime();
		}

	}
}
