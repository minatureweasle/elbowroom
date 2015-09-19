using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;

    //store a static instance for easy access of this class's public functions from other classes
	void Awake () {
		//the first ScoreManager to find that instance is null/unassigned, gets assigned to instance. 
		//Any others at any time will find that instance isn't null, it has already been assigned, and destroy themselves
		if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
		else
			Destroy (gameObject);
	}

    //retrieve the previous score of a given level and convert it into a string
	public string getLastScoreAsString(string levelName){
		if (GetLastScore(levelName) == -1)
			return "?";
		else
			return GetLastScore(levelName) + "";
	}

    //get the name of the winner of a given level and convert it into a string
	public string getWinner(string levelName){
		if (PlayerPrefs.GetString(levelName) == ""){
			return "?";
		}
		return PlayerPrefs.GetString(levelName);
	}

    //retrieve the best score of a given level and convert it into a string
	public string getBestScoreAsString(string levelName){
		if (GetBestScore(levelName) == -1)
			return "?";
		else
			return GetBestScore(levelName) + "";
	}

    //retrieve the previous score of a given level
	private float GetLastScore(string levelName){
		//initialise it if it doesnt exist yet
		if (!PlayerPrefs.HasKey (levelName + "lastScore"))
			PlayerPrefs.SetFloat (levelName + "lastScore", -1);

		return PlayerPrefs.GetFloat (levelName + "lastScore");
	}

    //retrieve the best score of a given level
	private float GetBestScore(string levelName){
		//initialise it if it doesnt exist yet
		if (!PlayerPrefs.HasKey (levelName + "bestScore"))
			PlayerPrefs.SetFloat (levelName + "bestScore", -1);
		
		return PlayerPrefs.GetFloat (levelName + "bestScore");
	}

    //write a score to storage for a given level
	void WriteLastScoreToStorage(string levelName, float scoreToWrite){
		PlayerPrefs.SetFloat (levelName + "lastScore", scoreToWrite);
	}

    //write the best score to storage for a given level
	void WriteBestScoreToStorage(string levelName, float scoreToWrite){
		PlayerPrefs.SetFloat (levelName + "bestScore", scoreToWrite);
	}

    //write the name of the winner to storage for a given level
	public void WriteWinnerName(string levelName, string winnerName){
		PlayerPrefs.SetString (levelName, winnerName); 
	}

    //make sure the given score is written to storage, 
    //and if its the new best overwrite the previous best
	public void setLastScore(string levelName, float newScore){
		//lastScore = newScore;
		WriteLastScoreToStorage(levelName, newScore);

		//Is this your new best?
		if (newScore < GetBestScore(levelName) || GetBestScore(levelName) == -1) {
			WriteBestScoreToStorage(levelName, newScore);
		}
	}
}
