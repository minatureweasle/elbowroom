using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutomaticLevel : MonoBehaviour {

    private Queue<string> level; 

    //initial parameters
    public int numIterations;
    public int numPlatforms;
    public float distRoom = 104;
	public float distPlatform = 11;
	public float distEndPlatform = 28;
	public float distanceBetweenObjects; 
    //is level constructed
    bool is_constructed = false;

    //to keep track of where to spawn objects
    public float creationDistance;
    private float spawnPoint;

	float spawnPointX = 0;
	float spawnPointY = 0;

	public float randomnessX = 0;
	public float randomnessY = 0;

    //objects to spawn
    public GameObject[] rooms;
    public GameObject platform; 
	public GameObject end_platform;
    
	// Use this for initialization
	void Start () {
        level = new Queue<string>();
        buildLevel();
		float spawnIncrement = pawp();
		spawnPoint += spawnIncrement;
	}
	
	// Generates the next object if the player is a certain 
    // distance from the last spawned object
	void Update () {
        if (is_constructed == false) {
            Transform players = PlayerGroup.instance.players;
            for (int i = 0; i < players.childCount; i++)
            {
                float playerZPos = players.GetChild(i).position.z;
                float distanceFromSpawn = spawnPoint - playerZPos;
				if (distanceFromSpawn < creationDistance)
                {
                    float spawnIncrement = pawp();
                    if (spawnIncrement > 0)
                    {
                        spawnPoint += spawnIncrement;
                    }
                    else
                    {
                        is_constructed = true;
                        break;
                    }
                }
            }        
        }   
	}
  

    //adds to the level queue the amount of rooms and platforms specified
    void buildLevel()
    {
        for (int i = 0; i < numIterations; i++)
        {
            Debug.Log("Building level");
            level.Enqueue("Room");
            for (int j = 0; j < numPlatforms; j++)
            {
                level.Enqueue("Platform");
            }
        }
        level.Enqueue("End");
    }
    //removes an element from the level queue and spawns the correct game object
    float pawp()
    {
        Debug.Log(level.Count);
        string ele = level.Dequeue();
        float spawnIncrement = -1;
        if (ele == "Room")
        {
			spawnPoint += distRoom/2;
			Instantiate(rooms[0], new Vector3(spawnPointX,spawnPointY,spawnPoint), Quaternion.identity);
            spawnIncrement = distRoom/2;
			spawnIncrement += distanceBetweenObjects; 
		}
        else if (ele == "Platform")
        {
			spawnPoint += distPlatform/2;
			Instantiate(platform, new Vector3(spawnPointX,spawnPointY,spawnPoint), Quaternion.identity);
            spawnIncrement = distPlatform/2;
			spawnIncrement += distanceBetweenObjects; 
        }
        else if (ele == "End")
        {
			spawnPoint += distEndPlatform/2;
			GameObject endPlatform = (GameObject)Instantiate(end_platform, new Vector3(spawnPointX,spawnPointY,spawnPoint), Quaternion.identity);
			endPlatform.GetComponentInChildren<DoorPortal>().room = "HomeRoom";
        }
		spawnPointX += Random.Range(-randomnessX,randomnessX);
		spawnPointY += Random.Range(-randomnessY,randomnessY);
        return spawnIncrement;
    }
}
