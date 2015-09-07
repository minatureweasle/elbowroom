using UnityEngine;
using System.Collections;

public class WallTrap : MonoBehaviour {

	float nextWallChangeTime = Mathf.Infinity;
	public float wallChangeInterval = 1;

	public Transform [] walls;
	bool [] shouldRise;
	int currentWall = 0;

	float initialHeight;

	void Start () {
		shouldRise = new bool[walls.Length];
		shouldRise [0] = false;
		initialHeight = walls[0].position.y;
		ChangeWalls();
	}

	void Update () {
		MoveWalls ();
		if (Time.time > nextWallChangeTime)
			ChangeWalls();
	}

	//the current wall is flagged to rise, the next wall is flagged to fall
	void ChangeWalls(){
		shouldRise[currentWall] = true;
		currentWall = (currentWall + 1)%walls.Length;
		shouldRise[currentWall] = false;
		nextWallChangeTime = Time.time + wallChangeInterval;
	}

	//increase or decrease height of each wall depending on its flag
	void MoveWalls(){
		for (int i = 0; i < walls.Length; i++) {
			if (shouldRise[i]){
				Vector3 destination = walls[i].position;
				destination.y = initialHeight;
				walls[i].position = Vector3.Lerp(walls[i].position, destination, 10f*Time.deltaTime);
			}
			else{
				Vector3 destination = walls[i].position;
				destination.y = initialHeight -6;
				walls[i].position = Vector3.Lerp(walls[i].position, destination, 10f*Time.deltaTime);
			}
		}
	}
}
