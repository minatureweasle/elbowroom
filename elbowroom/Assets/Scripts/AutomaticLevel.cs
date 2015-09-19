using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutomaticLevel : MonoBehaviour {

    private Queue<string> level; 

    //initial parameters
    public int numIterations;
    public int numPlatforms;
    public int distRoom;
    public int distPlatform;

    //to keep track of where to spawn objects
    public float creationDistance;
    private float spawnPoint;

    //objects to spawn
    public GameObject[] rooms;
    public GameObject platform; 
    
	// Use this for initialization
	void Start () {
        level = new Queue<string>();
        buildLevel();
	}
	
	// Generates the next object if the player is a certain 
    // distance from the last spawned object
	void Update () {
        Transform players = PlayerGroup.instance.players;
        for (int i = 0; i < players.childCount; i++)
        {
            float playerZPos = players.GetChild(i).position.z;
            float distanceFromSpawn = Mathf.Abs(spawnPoint - playerZPos);
            if (distanceFromSpawn > creationDistance)
            {
                float spawnIncrement = pawp();
                if (spawnIncrement > 0)
                {
                    spawnPoint += spawnIncrement;
                }
                else
                {
                    Debug.Log("Error in removing element from queue");
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
    }
    //removes an element from the level queue and spawns the correct game object
    float pawp()
    {
        Debug.Log(level.Count);
        string ele = level.Dequeue();
        float spawnIncrement = -1;
        if (ele == "Room")
        {
            Instantiate(rooms[0], new Vector3(0,0,spawnPoint), Quaternion.identity);
            spawnIncrement = distRoom;
        }
        else if (ele == "Platform")
        {
             Instantiate(platform, new Vector3(0,0,spawnPoint), Quaternion.identity);
             spawnIncrement = distPlatform;
        }
        return spawnIncrement;
    }
}
