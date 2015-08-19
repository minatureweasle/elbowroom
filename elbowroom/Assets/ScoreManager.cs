using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;

	float lastScore = -1;
	float bestScore = -1;

	void Awake () {

		//the first ScoreManager to find that instance is null/unassigned, gets assigned to instance. 
		//Any others at any time will find that instance isn't null, it has already been assigned, and destroy themselves
		if (instance == null)
			instance = this;
		else
			Destroy (gameObject);

		ReadStoredLastScore ();
		ReadStoredBestScore ();


	}

	public string getLastScoreAsString(){
		if (lastScore == -1)
			return "";
		else
			return lastScore + "";
	}

	public string getBestScoreAsString(){
		if (bestScore == -1)
			return "";
		else
			return bestScore + "";
	}

	void ReadStoredLastScore(){
		//initialise it if it doesnt exist yet
		if (!PlayerPrefs.HasKey ("lastScore"))
			PlayerPrefs.SetFloat ("lastScore", -1);
		else
			lastScore = PlayerPrefs.GetFloat ("lastScore");
	}

	void ReadStoredBestScore(){
		//initialise it if it doesnt exist yet
		if (!PlayerPrefs.HasKey ("bestScore"))
			PlayerPrefs.SetFloat ("bestScore", -1);
		else
			bestScore = PlayerPrefs.GetFloat ("bestScore");
	}

	void WriteLastScoreToStorage(){
		PlayerPrefs.SetFloat ("lastScore", lastScore);
	}
	
	void WriteBestScoreToStorage(){
		PlayerPrefs.SetFloat ("bestScore", bestScore);
	}

	void ClearScoresFromStorage(){
		PlayerPrefs.DeleteKey ("lastScore");
		PlayerPrefs.DeleteKey ("bestScore");
	}

	public void setLastScore(float newScore){
		lastScore = newScore;
		WriteLastScoreToStorage();

		//Is this your new best? Or do you not have a best yet?
		if (lastScore < bestScore || bestScore == -1) {
			bestScore = lastScore;
			WriteBestScoreToStorage();
		}
	}
}
