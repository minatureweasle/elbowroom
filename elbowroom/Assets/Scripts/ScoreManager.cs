using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;

	//float lastScore = -1;
	//float bestScore = -1;

	//Hashtable lastScores;
	//Hashtable bestScores;

	void Awake () {

		//the first ScoreManager to find that instance is null/unassigned, gets assigned to instance. 
		//Any others at any time will find that instance isn't null, it has already been assigned, and destroy themselves
		if (instance == null)
			instance = this;
		else
			Destroy (gameObject);

		//ReadStoredLastScore ();
		//ReadStoredBestScore ();


	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Delete))
			ClearScoresFromStorage ();
	}

	public string getLastScoreAsString(string levelName){
		if (GetLastScore(levelName) == -1)
			return "?";
		else
			return GetLastScore(levelName) + "";
	}

	public string getWinner(string levelName){
		if (PlayerPrefs.GetString(levelName) == ""){
			return "?";
		}
		return PlayerPrefs.GetString(levelName);
	}

	public string getBestScoreAsString(string levelName){
		if (GetBestScore(levelName) == -1)
			return "?";
		else
			return GetBestScore(levelName) + "";
	}

	private float GetLastScore(string levelName){
		//initialise it if it doesnt exist yet
		if (!PlayerPrefs.HasKey (levelName + "lastScore"))
			PlayerPrefs.SetFloat (levelName + "lastScore", -1);

		return PlayerPrefs.GetFloat (levelName + "lastScore");
	}

	private float GetBestScore(string levelName){
		//initialise it if it doesnt exist yet
		if (!PlayerPrefs.HasKey (levelName + "bestScore"))
			PlayerPrefs.SetFloat (levelName + "bestScore", -1);
		
		return PlayerPrefs.GetFloat (levelName + "bestScore");
	}

	/*void ReadStoredLastScore(){
		//initialise it if it doesnt exist yet
		if (!PlayerPrefs.HasKey ("lastScore"))
			PlayerPrefs.SetFloat ("lastScore", -1);

		//then retrieve the score from storage
		lastScore = PlayerPrefs.GetFloat ("lastScore");

		//go through settings and get each scene name. 
		//then check if scores are stored for that scene, and load them into the hashtable
		foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
		{
			if (S.enabled)
			{
				string name = S.path.Substring(S.path.LastIndexOf('/')+1);
				name = name.Substring(0,name.Length-6);
			}
		}
	}

	void ReadStoredBestScore(){
		//initialise it if it doesnt exist yet
		if (!PlayerPrefs.HasKey ("bestScore"))
			PlayerPrefs.SetFloat ("bestScore", -1);

		bestScore = PlayerPrefs.GetFloat ("bestScore");
	}*/

	void WriteLastScoreToStorage(string levelName, float scoreToWrite){
		PlayerPrefs.SetFloat (levelName + "lastScore", scoreToWrite);
	}
	
	void WriteBestScoreToStorage(string levelName, float scoreToWrite){
		PlayerPrefs.SetFloat (levelName + "bestScore", scoreToWrite);
	}

	public void WriteWinnerName(string levelName, string winnerName){
		PlayerPrefs.SetString (levelName, winnerName); 
	}

	public void setLastScore(string levelName, float newScore){
		//lastScore = newScore;
		WriteLastScoreToStorage(levelName, newScore);

		//Is this your new best?
		if (newScore < GetBestScore(levelName) || GetBestScore(levelName) == -1) {
			WriteBestScoreToStorage(levelName, newScore);
		}
	}

	void ClearScoresFromStorage(){
		PlayerPrefs.DeleteKey ("lastScore");
		PlayerPrefs.DeleteKey ("bestScore");
	}
}
